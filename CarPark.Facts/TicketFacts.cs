using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarPark.Facts
{
    public class TicketFacts // Top-most level class (Public / Internal)
    {
        public class General // inner class / nested class can has all 5 access modifiers
        {
            [Fact]
            public void BasicUsage()
            {
                // arrange -> จัดเตรียมสิ่งที่ต้องการเทส
                Ticket t;

                t = new Ticket();
                t.PlateNo = "8315";
                t.DateIn = new DateTime(2016, 4, 4, 9, 0, 0); // 9am
                t.DateOut = DateTime.Parse("13:30"); // 1:30pm

                // act

                // assert ตรวจสอบ
                Assert.Equal("8315", t.PlateNo);
                Assert.Equal(9, t.DateIn.Hour);
                Assert.Equal(13, t.DateOut.Value.Hour);
            }

            [Fact]
            public void NewTicket_HasNoDateOut()
            {
                var t = new Ticket();

                Assert.Null(t.DateOut);
            }
        }

        public class ParkingFeeProperty
        {
            [Fact]
            public void NewTicket_DontKnowParkingFee()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = null;

                Assert.Null(t.ParkingFee);
            }

            [Fact]
            public void First15Minutes_Free()
            {
                // arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15");

                // act
                decimal? fee = t.ParkingFee;

                // assert
                Assert.Equal(0m, fee);
            }

            [Fact]
            public void WithInFirst3Hours_50Baht()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15:01");

                decimal? fee = t.ParkingFee;

                Assert.Equal(50m, fee);
            }

            [Fact]
            public void WithInFirst3HoursII_50Baht()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("12:11");

                decimal? fee = t.ParkingFee;

                Assert.Equal(50m, fee);
            }

            [Fact]
            public void For4Hours_80Baht()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("13:00");

                decimal? fee = t.ParkingFee;

                Assert.Equal(80m, fee);
            }

            [Fact]
            public void For6Hours_140Baht()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("15:00");

                decimal? fee = t.ParkingFee;

                Assert.Equal(140m, fee);
            }

            [Fact]
            public void For6HoursExceed15Minutes_GetExtraHour()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("15:15:01");

                decimal? fee = t.ParkingFee;

                Assert.Equal(170m, fee);
            }

            [Fact]
            public void DateOutIsBeforeDateIn_ThrowsException()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("15:00");
                t.DateOut = DateTime.Parse("8:15:01");

                var ex = Assert.Throws<Exception>(() =>
                {
                    var fee = t.ParkingFee;
                });

                Assert.Equal("Invalid DateOut < DateIn.", ex.Message);
            }

        }
    }
}
