namespace Hotel.Core.Application.Features.Hotels.Queries.GetFilteredHotelsQuery
{
    //public class GetFilteredHotelsQueryHandler : IRequestHandler<GetFilteredHotelsQuery, Response<List<HotelDto>>>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public GetFilteredHotelsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<Response<List<HotelDto>>> Handle(GetFilteredHotelsQuery request, CancellationToken cancellationToken)
    //    {
    //        var city = await _unitOfWork.Cities.Where(c => c.Name.Contains(request.CityName!));

    //        var hotels = await _unitOfWork.Hotels.Where(h => city.Select(c => c.Id).AsEnumerable().Contains(h.CityId));



    //    }
    //}
}
