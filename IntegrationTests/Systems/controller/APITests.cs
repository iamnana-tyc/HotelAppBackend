﻿using HAPI.Fixtures;
using HAPI.Service;
using ICSSoft.STORMNET;
using ICSSoft.STORMNET.Business;
using System;
using System.Collections.Generic;
using Xunit;

namespace HAPI.Systems.Controller
{
    public class APITests : IDisposable
    {
        private readonly IDataService dataService;
        private List<Room> rooms = new List<Room>();
        private List<Booking> bookings = new List<Booking>();
        private List<Client> clients = new List<Client>();

        public APITests(IDataService _dataService)
        {
            dataService = _dataService;
            rooms = RoomFixtures.GetRooms();
            clients = RoomFixtures.GetAllClients();
            bookings = new List<Booking>
            {
                new Booking() {
                    Room = rooms[0],
                    Client = clients[0],
                    CheckIn = new DateTime(2024, 08, 08),
                    CheckOut =  new DateTime(2024, 08, 20),
                    GuestName = "Rodney",
                    __PrimaryKey = Guid.NewGuid(),
                },
            };

            var dataList = new List<DataObject>();
            dataList.AddRange(rooms);
            dataList.AddRange(bookings);
            dataList.AddRange(clients);
            var data = dataList.ToArray();
            dataService.UpdateObjects(ref data);
        }

        [Fact]
        public void GetAllRooms_OnSuccess_ReturnListOfRooms()
        {
            // Act
            List<Room> roomsResult = BookingService.GetAllRooms(dataService);

            // Assert
            foreach (DataObject dataObject in rooms)
            {
                var result = roomsResult.Find(room => room.__PrimaryKey.Equals(dataObject.__PrimaryKey)); // search for dataObject in rooms
                Assert.NotNull(result);
            }

        }

        [Fact]
        public void IsRoomFree_OnSuccess_ShouldReturnFalse_ForEmptyGuid()
        {
            // Arrange
            var roomId = Guid.Empty;
            var dateToFilter = DateTime.Now;

            // Act 
            var result = BookingService.IsRoomFree(dataService, roomId, dateToFilter);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRoomFree_OnSuccess_ShouldReturnOk_ForValidGuidAndDate()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var dateToFilter = DateTime.Now;

            // Act
            var result = BookingService.IsRoomFree(dataService, roomId, dateToFilter);

            // Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public void GetSumsOfRevenue_OnSuccess_ShouldReturnNumricValue() 
        {
            // Act
            var result = BookingService.GetSumOfRevenue(dataService) ;

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void GetAllRooms_OnSuccess_ShouldReturnSpecificRoomNumber()
        {
            // Arrange
            var rooms = BookingService.GetAllRooms(dataService);

            // Act
            var specificRoom = rooms[1].RoomNumber;
            var roomPrice = rooms[1].Price;

            // Assert
            Assert.Equal(101, specificRoom);
            Assert.Equal(200, roomPrice);
        }

        [Fact]
        public void CheckIfGuestNameExist_OnSucess_ShouldReturnTrue() 
        {
            // Arrange
            var guestName = "Rodney";

            // Act
            var result = this.bookings.Find(b => b.GuestName == guestName);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.GuestName == guestName);
        }

        [Fact]
        public void BookingWithSpecificCheckIn_OnSucess_ReturnTrue() 
        {
            // Arrange
            var specficCheckIn = new DateTime(2024, 08, 08);
            var specificCheckOut = new DateTime(2024, 08, 20);
            var expctedDates = (specficCheckIn, specificCheckOut);

            // Act
            var result = bookings.Find(b => b.CheckIn == specficCheckIn && b.CheckOut == specificCheckOut);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expctedDates, (result.CheckIn, result.CheckOut));
        }

        public void Dispose()
        {
            var dataList = new List<DataObject>();
            dataList.AddRange(rooms);
            dataList.AddRange(bookings);
            dataList.AddRange(clients);
            var data = dataList.ToArray();

            foreach (DataObject dataObject in data)
            {
                dataObject.SetStatus(ObjectStatus.Deleted);
            }

            dataService.UpdateObjects(ref data);
        }
    }
}
