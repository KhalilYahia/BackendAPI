
using YaznGhanem.Common;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Data.SeedData
{
    public class MyDbContextSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CustomRole>().HasData(
                   new CustomRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = Roles.AdminRole, ConcurrencyStamp = "1", NormalizedName = Roles.AdminRole }
           );
            modelBuilder.Entity<CustomRole>().HasData(
                 new CustomRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895712", Name = Roles.AdvertiserUserRole, ConcurrencyStamp = "2", NormalizedName = Roles.AdvertiserUserRole }
         );
            modelBuilder.Entity<CustomRole>().HasData(
                 new CustomRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895713", Name = Roles.DeveloperRole, ConcurrencyStamp = "3", NormalizedName = Roles.DeveloperRole }
         );
            modelBuilder.Entity<CustomRole>().HasData(
                new CustomRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895714", Name = Roles.NormalUserRole, ConcurrencyStamp = "4", NormalizedName = Roles.NormalUserRole }
        );


            var user = new CustomUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin",
                NormalizedUserName = ("Admin").ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = ("admin@gmail.com").ToUpper(),
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };
            PasswordHasher<CustomUser> passwordHasher = new PasswordHasher<CustomUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "1q2w!Q@W"); // old password Im$trongPassw0rd
            modelBuilder.Entity<CustomUser>().HasData(user);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
               );

            var Normaluser = new CustomUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843yy",
                UserName = "normal",
                NormalizedUserName = ("normal").ToUpper(),
                Email = "normal@gmail.com",
                NormalizedEmail = ("normal@gmail.com").ToUpper(),
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            Normaluser.PasswordHash = passwordHasher.HashPassword(user, "123yyy123"); // old password Im$trongPassw0rd
            modelBuilder.Entity<CustomUser>().HasData(Normaluser);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895714", UserId = "b74ddd14-6340-4840-95c2-db12554843yy" }
               );



            modelBuilder.Entity<Language>().HasData(
              new Language() { Id = 1, ArabicName = "العربية", EnglishName = "Arabic", Code = "ar" }
              );
            modelBuilder.Entity<Language>().HasData(
              new Language() { Id = 2, ArabicName = "الانكليزية", EnglishName = "English", Code = "en" }
              );

            #region catigory

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Sort = 1, CategoryName = "فواكه", Unit = "كغ" },
               new Category { Id = 2, Sort = 2, CategoryName = "صناديق وعبوات", Unit = "قطعة" },
               new Category { Id = 3, Sort = 3, CategoryName = "كرستا", Unit = "كغ" },
               new Category { Id = 4, Sort = 4, CategoryName = "طبالي", Unit = "قطعة" },
               new Category { Id = 5, Sort = 5, CategoryName = "شمع", Unit = "لتر" },
               new Category { Id = 6, Sort = 6, CategoryName = "مواد مرافقة للشمع", Unit = "ليتر" },
               new Category { Id = 7, Sort = 8, CategoryName = "مواد تخمير", Unit = "لتر" },
               new Category { Id = 8, Sort = 10, CategoryName = "عدد وأدوات ومحركات", Unit = "قطعة" },
               new Category { Id = 9, Sort = 12, CategoryName = "زينة بالكيس", Unit = "كيس" },
               new Category { Id = 10, Sort = 4, CategoryName = "بلاستيك", Unit = "قطعة" }
       );

            #endregion

            #region RepositoryMaterials

            // Seed data for the RepositoryMaterials table
            modelBuilder.Entity<RepositoryMaterials>().HasData(
                new RepositoryMaterials { Id = 1, CategoryId = 2, Name = "روسي كبير عبوة", Sort = 2, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 2, CategoryId = 2, Name = "روسي صغير عبوة", Sort = 3, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 3, CategoryId = 2, Name = "صناديق حقلية", Sort = 1, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 4, CategoryId = 2, Name = "عماني هرم صغير عبوة", Sort = 4, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 5, CategoryId = 2, Name = "عماني هرم كبير عبوة", Sort = 5, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 6, CategoryId = 2, Name = "نصف حقلي عبوة", Sort = 6, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 7, CategoryId = 2, Name = "ربع حقلي عبوة", Sort = 7, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 8, CategoryId = 2, Name = "ربع الربع عبوة", Sort = 8, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 9, CategoryId = 2, Name = "حقلي توضيب عبوة", Sort = 9, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 10, CategoryId = 2, Name = "كرتون توضيب عبوة", Sort = 10, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 11, CategoryId = 2, Name = "فلين عبوة", Sort = 11, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 12, CategoryId = 2, Name = "كيس عبوة", Sort = 12, DefaultPrice = 0, DefaultSoldPrice = 0 },

                new RepositoryMaterials { Id = 13, CategoryId = 3, Name = "زوايا", Sort = 13, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 14, CategoryId = 3, Name = "تحزيم", Sort = 14, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 15, CategoryId = 3, Name = "حبسات", Sort = 15, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 16, CategoryId = 3, Name = "ورق مطبوع", Sort = 16, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 17, CategoryId = 3, Name = "كرتون مطبوع", Sort = 17, DefaultPrice = 0, DefaultSoldPrice = 0 },

                new RepositoryMaterials { Id = 18, CategoryId = 1, Name = "أبو صرة متنوع", Sort = 18, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 19, CategoryId = 1, Name = "أبو صرة حمرا", Sort = 19, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 20, CategoryId = 1, Name = "أبو صرة بلدية", Sort = 20, DefaultPrice = 200, DefaultSoldPrice = 250 },
                new RepositoryMaterials { Id = 21, CategoryId = 1, Name = "يافاوي", Sort = 21, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 22, CategoryId = 1, Name = "كرمنتينا فرنسية", Sort = 22, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 23, CategoryId = 1, Name = "كرمنتينا بلدي", Sort = 23, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 24, CategoryId = 1, Name = "كرمنتينا عسلية", Sort = 24, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 25, CategoryId = 1, Name = "كرمنتينا طرابلسية", Sort = 25, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 26, CategoryId = 1, Name = "فراشة", Sort = 26, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 27, CategoryId = 1, Name = "يوسف أفندي", Sort = 27, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 28, CategoryId = 1, Name = "وجن", Sort = 28, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 29, CategoryId = 1, Name = "أورتينيك", Sort = 29, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 30, CategoryId = 1, Name = "حامض أمريكي", Sort = 30, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 31, CategoryId = 1, Name = "حامض بلدي", Sort = 31, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 32, CategoryId = 1, Name = "حامض ماير", Sort = 32, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 33, CategoryId = 1, Name = "حامض حلو", Sort = 33, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 34, CategoryId = 1, Name = "حامض سانتاريزا", Sort = 34, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 35, CategoryId = 1, Name = "دموي نظامي", Sort = 35, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 36, CategoryId = 1, Name = "دموي كذاب", Sort = 36, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 37, CategoryId = 1, Name = "ختملي", Sort = 37, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 38, CategoryId = 1, Name = "كريفون أبيض", Sort = 38, DefaultPrice = 0, DefaultSoldPrice = 0 },

                new RepositoryMaterials { Id = 39, CategoryId = 1, Name = "كريفون زهري", Sort = 39, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 40, CategoryId = 1, Name = "كريفون دموي", Sort = 40, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 41, CategoryId = 1, Name = "أبو ميلو", Sort = 41, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 42, CategoryId = 1, Name = "سوما", Sort = 42, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 43, CategoryId = 1, Name = "تفاح", Sort = 43, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 44, CategoryId = 1, Name = "رمان", Sort = 44, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 45, CategoryId = 1, Name = "بطاطا", Sort = 45, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 46, CategoryId = 1, Name = "موز", Sort = 46, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 47, CategoryId = 1, Name = "كرز", Sort = 47, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 48, CategoryId = 1, Name = "خوخ", Sort = 48, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 49, CategoryId = 1, Name = "مشمش", Sort = 49, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 50, CategoryId = 1, Name = "لنغا", Sort = 50, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 51, CategoryId = 1, Name = "مندلينا", Sort = 51, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 52, CategoryId = 1, Name = "بلانسيا", Sort = 52, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 53, CategoryId = 1, Name = "أفوكادو", Sort = 53, DefaultPrice = 0, DefaultSoldPrice = 0 },

                new RepositoryMaterials { Id = 54, CategoryId = 4, Name = "طبلية خشب", Sort = 54, DefaultPrice = 0, DefaultSoldPrice = 0 },
                new RepositoryMaterials { Id = 55, CategoryId = 4, Name = "طبلية حديد", Sort = 55, DefaultPrice = 0, DefaultSoldPrice = 0 });

            #endregion

            // Seed data for the TotalFunds table
            modelBuilder.Entity<TotalFunds>().HasData(
                new TotalFunds
                {
                    Id = 1,
                    TotalIn = 0,
                    TotalOut = 0,
                    CurrentFund = 0,
                    EarnedProfits=0,
                    Profits = 0
                }
            );

            modelBuilder.Entity<BoFTotal>().HasData(
                new BoFTotal
                {
                    Id = 1,
                    TotalIn = 0,
                    TotalOut = 0,
                    Current=0
                }
            );

            modelBuilder.Entity<Supplier>().HasData(
               new Supplier
               {
                   Id = 55,
                   SupplierName = "الشماعة",
                   SupplierNameWithoutSpaces = "الشماعة",
                  
               }
           );
        }


    }
}
