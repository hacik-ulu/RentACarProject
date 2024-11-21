using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentACarProject.Application.Features.Mediator.Queries.ReservationQueries;
using RentACarProject.Application.Features.Mediator.Results.ReservationResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetReservationByUserIdQueryHandler : IRequestHandler<GetReservationByUserIdQuery, List<GetReservationByUserIdQueryResult>>
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IRepository<AppUser> _appUserRepository;
    private readonly string _connectionString;

    public GetReservationByUserIdQueryHandler(
        IRepository<Reservation> reservationRepository,
        IRepository<AppUser> appUserRepository,
        IConfiguration configuration)
    {
        _reservationRepository = reservationRepository;
        _appUserRepository = appUserRepository;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<GetReservationByUserIdQueryResult>> Handle(GetReservationByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = request.Id;

        var appUser = await _appUserRepository.GetByIdAsync(userId); // AppUserID ile alıyoruz

        if (appUser == null)
        {
            return null; // Kullanıcı bulunamazsa null döner
        }

        var query = @"
            SELECT 
                r.ReservationID,
                r.Name,
                r.Surname,
                r.Email,
                r.Phone,
                pu.Name AS PickUpLocationName,
                du.Name AS DropOffLocationName,
                r.CarID,
                r.Description,
                c.Model AS CarModel,
                b.Name AS BrandName
            FROM 
                Reservations r
            JOIN 
                AppUsers a ON r.Name = a.Name AND r.Surname = a.Surname
            LEFT JOIN 
                Locations pu ON r.PickUpLocationID = pu.LocationID
            LEFT JOIN 
                Locations du ON r.DropOffLocationID = du.LocationID
            JOIN 
                Cars c ON r.CarID = c.CarID
            JOIN 
                Brands b ON c.BrandID = b.BrandID
            WHERE 
                a.AppUserID = @UserId";

        var results = new List<GetReservationByUserIdQueryResult>(); // Sonuçları tutacak liste

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) // Tüm sonuçları okuyalım
                    {
                        var result = new GetReservationByUserIdQueryResult
                        {
                            ReservationID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Surname = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone = reader.GetString(4),
                            PickUpLocationName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            DropOffLocationName = reader.IsDBNull(6) ? null : reader.GetString(6),
                            CarID = reader.GetInt32(7),
                            Description = reader.IsDBNull(8) ? null : reader.GetString(8),
                            CarModel = reader.GetString(9),
                            BrandName = reader.GetString(10)
                        };

                        results.Add(result); // Her sonucu listeye ekle
                    }
                }
            }
        }

        return results; // Sonuç listesini döndür
    }
}
