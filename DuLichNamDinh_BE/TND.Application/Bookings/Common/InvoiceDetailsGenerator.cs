using System.Text;
using TND.Domain.Entities;

namespace TND.Application.Bookings.Common
{
    public static class InvoiceDetailsGenerator
    {
        public static string GetInvoiceHtml(Booking booking)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(
                """
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Booking Invoice</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            margin: 20px;
                        }

                        table {
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 20px;
                        }

                        th, td {
                            border: 1px solid #dddddd;
                            text-align: left;
                            padding: 8px;
                        }

                        th {
                            background-color: #f2f2f2;
                        }

                        .total {
                            font-weight: bold;
                        }
                    </style>
                </head>
                """)
                .Append(
                $"""
                   <body>
                    <h1>Booking Invoice</h1>
                    <p>Hotel name: ${booking.Hotel.Name}</p>
                    <p>City name: ${booking.Hotel.City.Name}</p>
                    <p>Country name: ${booking.Hotel.City.Country}</p>
                    <p>Check-in date: ${booking.CheckInDateUtc}</p>
                    <p>Check-out date: {booking.CheckOutDateUtc}</p>
                    <p>Booking date: ${booking.BookingDateUtc}</p>
                   """)
                .Append(
                """
                    <table>
                    <thead>
                        <tr>
                            <th>Room number</th>
                            <th>Room type</th>
                            <th>Price</th>
                            <th>Discount %</th>
                            <th>Price After Discount</th>
                        </tr>
                    </thead>
                    <tbody>
                """);
            foreach(var invoiceRecord in booking.Invoice)
            {
                stringBuilder.Append(
                    $"""
                    <tr>
                        <td>${invoiceRecord.RoomNumber}</td>
                        <td>${invoiceRecord.RoomClassName}</td>
                        <td>${invoiceRecord.PriceAtBooking}</td>
                        <td>${invoiceRecord.DiscountPercentageAtBooking ?? 0}</td>
                        <td>${invoiceRecord.PriceAtBooking * (100 -
                                (invoiceRecord.DiscountPercentageAtBooking ?? 0) / 100)}</td>
                    </tr>
                    """
                    );
            }

            stringBuilder.Append(
                $"""
                </tbody>
                </table>

                <p class="total" Total Price: $${booking.TotalPrice}></p>
                </body>
                </html>
                """);

            return stringBuilder.ToString();
        }
    }
}
