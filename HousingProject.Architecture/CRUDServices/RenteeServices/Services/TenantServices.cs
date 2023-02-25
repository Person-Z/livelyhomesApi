﻿using HousingProject.Architecture.Data;
using HousingProject.Architecture.Interfaces.IEmail;
using HousingProject.Architecture.Interfaces.IRenteeServices;
using HousingProject.Architecture.IPeopleManagementServvices;
using HousingProject.Architecture.Response.Base;
using HousingProject.Architecture.ViewModel.People;
using HousingProject.Core.Models.Email;
using HousingProject.Core.Models.General;
using HousingProject.Core.Models.People;
using HousingProject.Core.Models.People.General;
using HousingProject.Core.Models.RentPayment;
using HousingProject.Core.ViewModel.Rentee;
using HousingProject.Core.ViewModel.Rentpayment;
using HousingProject.Infrastructure.ExtraFunctions.LoggedInUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingProject.Architecture.Services.Rentee.Services
{
    public class TenantServices : ITenantServices
    {
        private readonly HousingProjectContext _context;
        private readonly IEmailServices _iemailservvices;

        private readonly ILoggedIn _loggedIn;

        public readonly IRegistrationServices _registrationServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public TenantServices(
          HousingProjectContext context,
          IHttpContextAccessor httpContextAccessor,
          IEmailServices iemailservvices,
          IRegistrationServices registrationServices,
           ILoggedIn loggedIn


        )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _iemailservvices = iemailservvices;
            _registrationServices = registrationServices;
            _loggedIn = loggedIn;
        }

        // get loggin user
        public async Task<RegistrationModel> LoggedInUser()
        {
            var currentuserid =
              _httpContextAccessor
              .HttpContext
              .User
              .Claims
              .Where(x => x.Type == "Id")
              .Select(p => p.Value)
              .FirstOrDefault();
            var loggedinuser =
              await _context
              .RegistrationModel
              .Where(x => x.Id == currentuserid)
              .FirstOrDefaultAsync();

            return loggedinuser;
        }


        //
        public async Task<BaseResponse> Register_Rentee(Rentee_RegistrationViewModel RenteeVm)
        {
            var loggeinuserr = LoggedInUser().Result;
            if (_loggedIn.LoggedInUser().Result.Is_Tenant)
            {

                return new BaseResponse {Code="124", ErrorMessage="You do not have permision to do this" };
            }


            try
            {
                if(!(loggeinuserr.Is_Agent || loggeinuserr.Is_Landlord))

                {

                    return new BaseResponse { Code = "129" , ErrorMessage="You do not have permission to do this"};
                }


                if (RenteeVm.FirstName == "" && RenteeVm.LastName == "")

                {
                    return new BaseResponse
                    {
                        Code = "408",
                        ErrorMessage = " Names cann't be empty"
                    };
                }
                if (RenteeVm.Email == "")
                {
                    return new BaseResponse
                    {
                        Code = "409",
                        ErrorMessage = " Email cannot be empty"
                    };
                }
                var usermodel = new RegisterViewModel
                {

                    FirstName = RenteeVm.FirstName,
                    LasstName = RenteeVm.LastName,
                    Email = RenteeVm.Email,
                    IdNumber = RenteeVm.FirstName,
                   
                    PhoneNumber = RenteeVm.Agent_PhoneNumber,
                    Password = "Password@1234",
                    RetypePassword = "Password@1234",
                    BirthDate = "00/00/00",
                    Is_Tenant = true,


                };
               

             


                var resp = await _registrationServices.UserRegistration(usermodel);

                if (resp.Code == "200")
                {
                    var emailbody = new UserEmailOptions
                    {

                        UserName = RenteeVm.FirstName,
                        PayLoad = "sent mail test",
                        ToEmail = RenteeVm.Email
                    };

                    await _iemailservvices.SentdirectlytonewTenant(emailbody);

                }

                else
                {

                    return new BaseResponse { Code = "170", ErrorMessage = "Tenant not created as a user" };
                }



                var renteemodel = new TenantClass
                {

                    FirstName = RenteeVm.FirstName,
                    LastName = RenteeVm.LastName,
                    HouseFloor = RenteeVm.HouseFloor,
                    Email = RenteeVm.Email,
                    CreatedBy = loggeinuserr.Email,
                    Cars = RenteeVm.Cars,
                    ServicesFees = RenteeVm.ServicesFees,
                    Rentee_PhoneNumber = RenteeVm.Rentee_PhoneNumber,
                    BedRoom_Number = RenteeVm.BedRoom_Number,
                    House_Rent = RenteeVm.House_Rent,
                    Agent_PhoneNumber = RenteeVm.Agent_PhoneNumber,
                    BuildingCareTaker_PhoneNumber = RenteeVm.BuildingCareTaker_PhoneNumber,
                    HouseiD = RenteeVm.HouseiD,
                    Appartment_DoorNumber = RenteeVm.Appartment_DoorNumber,
                    Number0f_Occupants = RenteeVm.Number0f_Occupants,

                };





                _context.Add(renteemodel);
                await _context.SaveChangesAsync();
                var loggedinuser = LoggedInUser().Result;

                var emails = loggedinuser.FirstName;
                var creatorusername = LoggedInUser().Result;
                var sendbody = new UserEmailOptions
                {

                    UserName = loggedinuser.FirstName,
                    PayLoad = "sent mail test",
                    ToEmail = loggedinuser.Email
                };


                await _iemailservvices.newtenantemail(sendbody);

                return new BaseResponse
                {
                    Code = "200",
                    SuccessMessage = "Succesfully registered tenant",

                };
            }


            catch (Exception ex)
            {
                return new BaseResponse { Code = "300", ErrorMessage = ex.Message };
            }


        }

        public async Task<IEnumerable<TenantClass>> GetAllRenteess()
        {


            return await _context.TenantClass.OrderByDescending(x => x.DateCreated).ToListAsync();
        }


        //get element by id

        public async Task<BaseResponse> GetTenantById(int tenantId)
        {
            if (_loggedIn.LoggedInUser().Result.Is_Tenant)
            {

                return new BaseResponse { Code = "124", ErrorMessage = "You do not have permision to do this" };
            }


            var tenant = await _context.TenantClass.Where(x => x.RenteeId == tenantId).FirstOrDefaultAsync();


            try
            {
                if (tenant != null)
                {

                    return new BaseResponse { Code = "200", Body = tenant };
                }
                else
                {

                    return new BaseResponse { Code = "104", ErrorMessage = "The tenant doesn not exist" };
                }

            }
            catch (Exception ex)
            {

                foreach (var error in ex.Message)
                {

                    return new BaseResponse { Code = "016", ErrorMessage = error.ToString() };
                }
                return new BaseResponse { Code = "017", ErrorMessage = "Something else happened " };
            }
        }

        //
        public async Task<BaseResponse> GetTenantRentSum(int tenantId)
        {
            if (_loggedIn.LoggedInUser().Result.Is_Tenant)
            {

                return new BaseResponse { Code = "124", ErrorMessage = "You do not have permision to do this" };
            }


            var tenant = await _context.TenantClass.Where(x => x.RenteeId == tenantId).FirstOrDefaultAsync();


            try
            {
                if (tenant != null)
                {

                    return new BaseResponse { Code = "200", Body = tenant };
                }
                else
                {

                    return new BaseResponse { Code = "104", ErrorMessage = "The tenant doesn not exist" };
                }

            }
            catch (Exception ex)
            {

                foreach (var error in ex.Message)
                {

                    return new BaseResponse { Code = "016", ErrorMessage = error.ToString() };
                }
                return new BaseResponse { Code = "017", ErrorMessage = "Something else happened " };
            }
        }

        //

        public async Task<BaseResponse> GetTenantSummary(int houseId, int tenantId)
        {
            try
            {
                var user = LoggedInUser().Result;
                var gethouseid =
                  await _context
                  .House_Registration
                  .Where(x => x.HouseiD == houseId)
                  .FirstOrDefaultAsync();

                if (gethouseid == null)
                {
                    return new BaseResponse
                    {
                        Code = "000",
                        ErrorMessage = "The house does not exist"
                    };

                }

                var tenant =
                  await _context
                  .TenantClass
                  .Where(x => x.RenteeId == tenantId)
                  .FirstOrDefaultAsync();
                var arears = tenant.House_Rent - tenant.RentPaid;

                //if (arears >= 1)
                //{
                //    return new BaseResponse { Code = "200", SuccessMessage = $"You have  arrears of {arears} Ksh" };
                //}
                if (gethouseid.HouseiD == tenant.HouseiD)
                {
                    var summaryobjects = new TenantSummary
                    {
                        HouseiD = gethouseid.HouseiD,
                        AgentName = gethouseid.CreatorNames,
                        HouseDoornumber = tenant.Appartment_DoorNumber,
                        HouseRent = tenant.House_Rent,
                        RentArrears = tenant.House_Rent - tenant.RentPaid,
                        overpayment = tenant.RentOverpayment,
                        TenantId = tenant.RenteeId,
                        FirstName = tenant.FirstName,
                        LastName = tenant.LastName,

                    };

                    return new BaseResponse
                    {
                        Code = "200",
                        Body = summaryobjects
                    };
                }
                else if (gethouseid.HouseiD != tenant.HouseiD)
                {

                    return new BaseResponse
                    {
                        Code = "000",
                        ErrorMessage = "No  rent summary exists for such a tenant "
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorMessage = ex.Message
                };
            }
            return new BaseResponse { };
        }


        public async Task<BaseResponse> UpdateRentpaid(int tenantid, float rentadded)
        {
            if (_loggedIn.LoggedInUser().Result.Is_Tenant)
            {

                return new BaseResponse { Code = "124", ErrorMessage = "You do not have permision to do this" };
            }

            var tenant =
              await _context
              .TenantClass
              .Where(x => x.RenteeId == tenantid)
              .FirstOrDefaultAsync();
            if (tenant == null)
            {

                return new BaseResponse
                {
                    Code = "0000",
                    ErrorMessage = "Tenant does not exists"
                };
            }
            try
            {

                // var fullnames = tenant.FirstName + " " + tenant.LastName;
                tenant.RentPaid = +rentadded;

                _context.TenantClass.Update(tenant);

                await _context.SaveChangesAsync();

                return new BaseResponse
                {
                    Code = "200",
                    SuccessMessage = $"Successfully updated rent details for {rentadded}"
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    Code = "000",
                    ErrorMessage = ex.Message
                };
            }


        }


        public async Task<IEnumerable> GetTenantByHouseid(int houseid)
        {


            var houselist = await  _context.TenantClass.Where(x => x.HouseiD == houseid).OrderByDescending(x => x.DateCreated).ToListAsync();

            return houselist;

        }

        public async Task<BaseResponse> TenanttotalRent(int tenantId)
        {

            try
            {

                var tenantpaymenttable = await _context.Rentpayment.Where(x => x.TenantId == tenantId).ToListAsync();

                var tenantcount = tenantpaymenttable.Count;

                var tenantintenantclass = await _context.TenantClass.Where(x => x.RenteeId == tenantId).FirstOrDefaultAsync();

                var rentamount = tenantintenantclass.House_Rent;

                var renttopaid = rentamount * tenantcount;


                return new BaseResponse { Code = "200", Totals = Convert.ToInt32(renttopaid) };

            }
            catch (Exception ex)
            {

                foreach (var error in ex.Message)
                {

                    return new BaseResponse { Code = "110", ErrorMessage = error.ToString() };

                }


            }
            return new BaseResponse { Code = "120", ErrorMessage = "Something new happened" };

        }
        public async Task<BaseResponse> RentTotal(int tenantid)
        {

            try
            {

                var tenanttotalRent = await _context.Rentpayment.Where(x => x.TenantId == tenantid).Select(x => x.RentPaid).SumAsync();

                // var totalrents = tenanttotalRent.RentPaid;

                return new BaseResponse { Code = "200", Body = tenanttotalRent };

            }
            catch (Exception ex)
            {

                foreach (var error in ex.Message)
                {

                    return new BaseResponse { Code = "108", ErrorMessage = error.ToString() };
                }

                return new BaseResponse { Code = "110", ErrorMessage = "something foreign happened" };
            }
        }


        public async Task<IEnumerable> rentpaymentList(int tenantId)
        {


            var tenantlist = await _context.Rentpayment.Where(x => x.TenantId == tenantId).OrderByDescending(x => x.Datepaid).ToListAsync();

            return tenantlist;
        }

        public async Task<BaseResponse> updateTenantRent(int tenantId, RentpaymentViewmodel vm)
        {


            try
            {
               var tenant = await _context.TenantClass.Where(x => x.RenteeId == tenantId).FirstOrDefaultAsync();

                var sentbody = new Rentpayment
                {

                    Email = tenant.Email,
                    RentPaid = vm.RentPaid,
                    TenantId = tenant.RenteeId,
                    Month = vm.Month,
                    HouseId = tenant.HouseiD,

                };

                await _context.AddAsync(sentbody);
                await _context.SaveChangesAsync();
                return new BaseResponse { Code = "200", SuccessMessage = $"Rent updated successfully for {tenant.FirstName} {tenant.LastName}" };

            }
            catch (Exception e)
            {

                foreach (var error in e.Message)
                {

                    return new BaseResponse { Code = "106", ErrorMessage = error.ToString() };
                }

                return new BaseResponse { Code = "108", ErrorMessage = "something foreign happened" };
            }







        }

        public async Task<BaseResponse> GetLoggedInTenant()
        {

            var user = LoggedInUser().Result;
            try
            {
                if (user == null)
                {

                    return new BaseResponse { Code = "190", ErrorMessage = "something happened" };
                }

                var loggedinTenant = await _context.TenantClass.Where(x => x.Email == user.Email).FirstOrDefaultAsync();

                
                if (loggedinTenant == null)
                {

                    return new BaseResponse { Code = "140", ErrorMessage = "Tenant was not found " };
                }


                if (loggedinTenant.Email == user.Email)
                {

                    if (!user.Is_Tenant)
                    {
                        return new BaseResponse { Code = "235", ErrorMessage = "Kindly use the email sent to activate your Tenant account" };
                    }
                }
                return new BaseResponse { Code = "200", Body = loggedinTenant };
            }
            catch (Exception ex)
            {
                foreach (var error in ex.Message)
                {
                    return new BaseResponse { Code = "130", ErrorMessage = error.ToString() };
                }
            }
            return new BaseResponse { Code = "167", ErrorMessage = "A foreign error was encountered" };

        }

        public async Task<BaseResponse> GetLogeedInTenantHouse()
        {
            var user = LoggedInUser().Result;
            var loggedinTenant = await _context.TenantClass.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
            try
            {


                if (loggedinTenant == null)
                {
                    return new BaseResponse { Code = "345", ErrorMessage = "This tenant does not exist" };

                }
                var houseoftenant = await _context.House_Registration.Where(x => x.HouseiD == loggedinTenant.HouseiD).FirstOrDefaultAsync();

                if (houseoftenant == null)
                {
                    return new BaseResponse { Code = "140", ErrorMessage = "The tent is not registered unser any house" };
                }


                return new BaseResponse { Code = "200", Body = houseoftenant };
             }
            catch(Exception ex)
            {

                foreach(var error in ex.Message)
                {
                    return new BaseResponse { Code = "234", ErrorMessage = error.ToString() };
                }

            }
            return new BaseResponse { Code = "000", ErrorMessage = "Something foreighn happened" };
        }

       
    }
}