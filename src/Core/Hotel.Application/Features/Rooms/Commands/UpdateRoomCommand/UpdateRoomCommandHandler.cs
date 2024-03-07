using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Rooms.Commands.UpdateRoomCommand
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            await ValidateRoomData(request.HotelId, request.RoomTypeId);

            var oldRoom = await _unitOfWork.Rooms.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"La habitación con el id {request.Id} no existe");

            oldRoom.Name = request.Name;
            oldRoom.Taxes = request.Taxes;
            oldRoom.Available = request.Available;
            oldRoom.RoomNumber = request.RoomNumber;
            oldRoom.RoomTypeId = request.RoomTypeId;
            oldRoom.BaseCost = request.BaseCost;
            oldRoom.Floor = request.Floor;
            oldRoom.HotelId = request.HotelId;

            _unitOfWork.Rooms.Update(oldRoom);

            await _unitOfWork.SaveChangesAsync();

            return new Response<int>(oldRoom.Id, "Habitación modificada correctamente. ");
        }

        private async Task ValidateRoomData(int hotelId, int roomTypeId)
        {
            List<string> errors = new();

            var hotel = await _unitOfWork.Hotels.GetByIdAsync(hotelId);
            var roomType = await _unitOfWork.RoomTypes.GetByIdAsync(hotelId);

            if (hotel == null)
            {
                errors.Add($"El hotel con el id {hotelId} no existe, consulte los hoteles disponibles. ");
            }

            if (roomType == null)
            {
                errors.Add($"El tipo de abitción con el id {roomTypeId} no existe, consulte los tipos de habitaciones disponibles. ");
            }

            if (errors.Count > 0) throw new ValidationException(errors);
        }
    }
}
