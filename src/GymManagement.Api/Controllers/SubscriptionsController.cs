using GymManagement.Application.Commands.CreateSubscription;
using GymManagement.Application.Services;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionsController(ISender sender) : ControllerBase
{

    [HttpPost]
 public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
  {
    var command = new CreateSubscriptionCommand(
      request.SubscriptionType.ToString(),
      request.AdminId);

    var subscriptionId =await sender.Send(command);
      
    var response = new SubscriptionResponse(
      subscriptionId,
      request.SubscriptionType);
  
    return Ok(response);
  }   
}