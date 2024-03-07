using AutoMapper;
using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using Hotel.Core.Domain.Entities;
using MediatR;

namespace Hotel.Core.Application.Features.Travelers.Commands.CreateTravelerCommand
{
    public class CreateTravelerCommandHandler : IRequestHandler<CreateTravelerCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CreateTravelerCommandHandler(IUnitOfWork unitOfWork,
            IAccountService accountService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTravelerCommand request, CancellationToken cancellationToken)
        {
            await ValidateTravelerData(request.DocumentTypeId, request.GenderId);

            var user = _mapper.Map<SignUpRequestDto>(request);
            var traveler = _mapper.Map<Traveler>(request);

            var idUser = await _accountService.SignUpAsync(user);

            traveler.UserId = idUser;

            var travelerRegistred = await _unitOfWork.Travelers.SaveAsync(traveler);

            await _unitOfWork.SaveChangesAsync();

            return new Response<int>(travelerRegistred.Id, "Viejaro registrado correctamente. ");
        }

        private async Task ValidateTravelerData(int documentTypeId, int genderId)
        {
            List<string> errors = new();

            var documentType = await _unitOfWork.DocumentTypes.GetByIdAsync(documentTypeId);

            if (documentType == null)
            {
                errors.Add($"El tipo de documento con id {documentTypeId} no existe, consulte los tipos de documentos disponibles. ");
            }

            var gender = await _unitOfWork.Genders.GetByIdAsync(genderId);

            if (gender == null)
            {
                errors.Add($"El género con id {genderId} no existe, consulte los géneros disponibles. ");
            }

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
