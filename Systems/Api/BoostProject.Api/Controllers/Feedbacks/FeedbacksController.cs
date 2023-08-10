using AutoMapper;
using BoostProject.Api.Controllers.Feedbacks.Models;
using BoostProject.Common.Consts;
using BoostProject.Common.Security;
using BoostProject.Services.FeedbackService;
using BoostProject.Services.FeedbackService.Models;
using BoostProject.Services.UserAccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoostProject.Api.Controllers.Feedbacks;

[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors(PolicyName = CorsConsts.DefaultOriginName)]
[ApiVersion("1.0")]
[ApiController]
public class FeedbacksController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;
    private readonly IMapper _mapper;
    private readonly ILogger<FeedbacksController> _logger;

    public FeedbacksController(
        IFeedbackService feedbackService,
        IMapper mapper,
        ILogger<FeedbacksController> logger
        )
    {
        _feedbackService = feedbackService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<FeedbackResponse>> GetFeedbacks()
    {
        _logger.LogInformation("Returning all feedbacks..");

        var response = await _feedbackService.GetAllFeedbacks();

        return response;
    }

    [ProducesResponseType(typeof(RegisterUserAccountResponse), 200)]
    [HttpPost]
    [Authorize/*(Policy = AppScopes.FeedbackWrite)*/]
    public async Task<CreateFeedbackResponse> CreateFeedback([FromBody] CreateFeedbackRequest request)
    {
        _logger.LogInformation("Creating the feedback (UserId: {UserId})..", request.UserId);

        var model = _mapper.Map<CreateFeedbackModel>(request);

        var response = await _feedbackService.CreateFeedback(model);

        return response;
    }
}
