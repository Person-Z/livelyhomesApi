﻿// <auto-generated />
using System;
using HousingProject.Architecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HousingProject.Infrastructure.Migrations
{
    [DbContext(typeof(HousingProjectContext))]
    [Migration("20230119201114_Addingprofessions")]
    partial class Addingprofessions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HousingProject.Core.Models.CountiesModel.AddCounty", b =>
                {
                    b.Property<int>("CountyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AddedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatorID")
                        .HasColumnType("int");

                    b.Property<DateTime>("MyProperty")
                        .HasColumnType("datetime2");

                    b.HasKey("CountyId");

                    b.ToTable("AddCounty");
                });

            modelBuilder.Entity("HousingProject.Core.Models.General.TenantSummary", b =>
                {
                    b.Property<int>("SummaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AgentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfRentPayment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlatNumberId")
                        .HasColumnType("int");

                    b.Property<int>("HouseDoornumber")
                        .HasColumnType("int");

                    b.Property<float>("HouseRent")
                        .HasColumnType("real");

                    b.Property<int>("HouseiD")
                        .HasColumnType("int");

                    b.Property<int?>("HouseregistrationHouseiD")
                        .HasColumnType("int");

                    b.Property<bool>("IsLandlord")
                        .HasColumnType("bit");

                    b.Property<int>("LandlordId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OverDueRent")
                        .HasColumnType("bit");

                    b.Property<float>("RentArrears")
                        .HasColumnType("real");

                    b.Property<bool>("RentPaid")
                        .HasColumnType("bit");

                    b.Property<int?>("TenantClassRenteeId")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<float>("overpayment")
                        .HasColumnType("real");

                    b.HasKey("SummaryId");

                    b.HasIndex("HouseregistrationHouseiD");

                    b.HasIndex("LandlordId");

                    b.HasIndex("TenantClassRenteeId");

                    b.ToTable("TenantSummary");
                });

            modelBuilder.Entity("HousingProject.Core.Models.Houses.Flats.AdminContacts.AdminContacts", b =>
                {
                    b.Property<int>("contactsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("contactsId");

                    b.ToTable("AdminContacts");
                });

            modelBuilder.Entity("HousingProject.Core.Models.Houses.Flats.House_Registration.House_Registration", b =>
                {
                    b.Property<int>("HouseiD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailSent")
                        .HasColumnType("bit");

                    b.Property<int>("Estimated_Maximum_Capacity")
                        .HasColumnType("int");

                    b.Property<string>("House_Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("House_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner_Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner_LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Owner_id_Number")
                        .HasColumnType("int");

                    b.Property<int>("Total_Units")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HouseiD");

                    b.HasIndex("CreatedById");

                    b.ToTable("House_Registration");
                });

            modelBuilder.Entity("HousingProject.Core.Models.Houses.Flats.uploadImage.UploadImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("UploadImage");
                });

            modelBuilder.Entity("HousingProject.Core.Models.Houses.HouseUsers.HouseUsers", b =>
                {
                    b.Property<int>("HouseuserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("AccountActivated")
                        .HasColumnType("bit");

                    b.Property<string>("BirthDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Creatormail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseID")
                        .HasColumnType("int");

                    b.Property<string>("HouseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LasstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetypePassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salutation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HouseuserId");

                    b.ToTable("HouseUsers");
                });

            modelBuilder.Entity("HousingProject.Core.Models.ImagesModelsUsed.ImaageUploadClass", b =>
                {
                    b.Property<int>("imagedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("imagedId");

                    b.ToTable("ImaageUploadClass");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.General.ContactUs", b =>
                {
                    b.Property<int>("ContacusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Useremail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContacusId");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.General.TenantClass", b =>
                {
                    b.Property<int>("RenteeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Agent_PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Appartment_DoorNumber")
                        .HasColumnType("int");

                    b.Property<int>("BedRoom_Number")
                        .HasColumnType("int");

                    b.Property<string>("BuildingCareTaker_PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cars")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Email_Confirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseFloor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("House_RegistrationHouseiD")
                        .HasColumnType("int");

                    b.Property<float>("House_Rent")
                        .HasColumnType("real");

                    b.Property<int>("HouseiD")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number0f_Occupants")
                        .HasColumnType("int");

                    b.Property<float>("RentArrears")
                        .HasColumnType("real");

                    b.Property<float>("RentOverpayment")
                        .HasColumnType("real");

                    b.Property<float>("RentPaid")
                        .HasColumnType("real");

                    b.Property<string>("Rentee_PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServicesFees")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RenteeId");

                    b.HasIndex("House_RegistrationHouseiD");

                    b.ToTable("TenantClass");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.Landlord.Landlordmodel", b =>
                {
                    b.Property<int>("LandlordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LasstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LondLord_HouseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Paybill_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RentCollection_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Till_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LandlordId");

                    b.ToTable("Landlordmodel");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.RegistrationModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Agent")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_CareTaker")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Landlord")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Tenant")
                        .HasColumnType("bit");

                    b.Property<string>("LasstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Salutation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Registration");
                });

            modelBuilder.Entity("HousingProject.Core.Models.Professionals.RegisterProfessional", b =>
                {
                    b.Property<int>("ProfessionalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperationArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salutation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfessionalId");

                    b.ToTable("RegisterProfessional");
                });

            modelBuilder.Entity("HousingProject.Core.Models.RentPayment.RentDebit", b =>
                {
                    b.Property<int>("rentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Debit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HousedId")
                        .HasColumnType("int");

                    b.Property<string>("Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rentmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("rentID");

                    b.ToTable("RentDebit");
                });

            modelBuilder.Entity("HousingProject.Core.Models.RentPayment.Rentpayment", b =>
                {
                    b.Property<int>("RentpaidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Datepaid")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<string>("Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentPaid")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("RentpaidId");

                    b.ToTable("Rentpayment");
                });

            modelBuilder.Entity("HousingProject.Infrastructure.ExtraFunctions.AddCities", b =>
                {
                    b.Property<int>("SubCountyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountyId")
                        .HasColumnType("int");

                    b.HasKey("SubCountyId");

                    b.ToTable("AddCities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HousingProject.Core.Models.General.TenantSummary", b =>
                {
                    b.HasOne("HousingProject.Core.Models.Houses.Flats.House_Registration.House_Registration", "Houseregistration")
                        .WithMany()
                        .HasForeignKey("HouseregistrationHouseiD");

                    b.HasOne("HousingProject.Core.Models.People.Landlord.Landlordmodel", "Landlord")
                        .WithMany("Tenant")
                        .HasForeignKey("LandlordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HousingProject.Core.Models.People.General.TenantClass", null)
                        .WithMany("Summary")
                        .HasForeignKey("TenantClassRenteeId");

                    b.Navigation("Houseregistration");

                    b.Navigation("Landlord");
                });

            modelBuilder.Entity("HousingProject.Core.Models.Houses.Flats.House_Registration.House_Registration", b =>
                {
                    b.HasOne("HousingProject.Core.Models.People.RegistrationModel", "CreatedBy")
                        .WithMany("Houses_Registered")
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.General.TenantClass", b =>
                {
                    b.HasOne("HousingProject.Core.Models.Houses.Flats.House_Registration.House_Registration", "House_Registration")
                        .WithMany("Tenant")
                        .HasForeignKey("House_RegistrationHouseiD");

                    b.Navigation("House_Registration");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HousingProject.Core.Models.People.RegistrationModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HousingProject.Core.Models.People.RegistrationModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HousingProject.Core.Models.People.RegistrationModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HousingProject.Core.Models.People.RegistrationModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HousingProject.Core.Models.Houses.Flats.House_Registration.House_Registration", b =>
                {
                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.General.TenantClass", b =>
                {
                    b.Navigation("Summary");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.Landlord.Landlordmodel", b =>
                {
                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("HousingProject.Core.Models.People.RegistrationModel", b =>
                {
                    b.Navigation("Houses_Registered");
                });
#pragma warning restore 612, 618
        }
    }
}
