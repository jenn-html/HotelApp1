USE HotelJenni
SELECT * 
FROM Guests 
WHERE Guests.IsActive = 'true' 
ORDER BY Guests.FirstName;

USE HotelJenni
SELECT 
CONCAT(FirstName, ' ', LastName) AS 'Name',
Bookings.CheckIn AS 'Check-in date',
Bookings.CheckOut AS 'Check-out date',
CONCAT(Rooms.RoomNumber, ' ', RoomTypes.Type) AS 'Room'
FROM Guests 
JOIN Bookings ON Guests.Id = Bookings.GuestId
JOIN Rooms ON Bookings.RoomId = Rooms.Id
JOIN RoomTypes ON Rooms.RoomTypeId = RoomTypes.Id
WHERE Bookings.IsActive = '1' 
ORDER BY Guests.FirstName;

USE HotelJenni
SELECT 
Bookings.Id AS 'Booking id',
Bookings.TimeOfBooking AS 'Booking added',
Invoices.IsPayed AS 'Payed',
Invoices.LastDayToPay AS 'Last day to pay'
FROM Bookings
Join Invoices ON Bookings.InvoiceID = Invoices.ID
ORDER BY Invoices.LastDayToPay

USE HotelJenni
SELECT *
FROM Invoices
WHERE Invoices.IsPayed = '1' and (Invoices.IsActive = '1')
ORDER BY Invoices.ID;

USE HotelJenni
SELECT RoomNumber, PricePerNight 
FROM Rooms 
WHERE PricePerNight > (
    SELECT AVG(PricePerNight) 
    FROM Rooms
);

USE HotelJenni
SELECT RoomNumber, PricePerNight 
FROM Rooms 
WHERE PricePerNight < (
    SELECT AVG(PricePerNight) 
    FROM Rooms
);
USE HotelJenni
SELECT 
CONCAT(FirstName, ' ', LastName) AS 'Name', 
COUNT(Bookings.Id) AS 'Total Bookings'
FROM Guests
JOIN Bookings ON Guests.Id = Bookings.GuestId
GROUP BY Guests.FirstName, Guests.LastName;