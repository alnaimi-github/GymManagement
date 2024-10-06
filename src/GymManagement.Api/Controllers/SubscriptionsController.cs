using GymManagement.Api.Controllers;
using GymManagement.Application.Commands.CreateSubscription;
using GymManagement.Application.Queries.GetSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api;

[Route("subscriptions")]
public class SubscriptionsController(ISender sender) : ApiController
{

    [HttpPost]
 public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
  {
    if(!DomainSubscriptionType.TryFromName(
      request.SubscriptionType.ToString(),
      out var subscriptionType))
    {
        return Problem(
          statusCode: StatusCodes.Status400BadRequest,
          detail: "Invalid subscription type");
    }
   
    var command = new CreateSubscriptionCommand(
                  subscriptionType,
                  request.AdminId);

    var createSubscriptionResult =await sender.Send(command);
      
    return createSubscriptionResult.Match(
        subscription => CreatedAtAction(
            nameof(GetSubscription),
            new { subscriptionId = subscription.Id },
            new SubscriptionResponse(
                subscription.Id,
                ToDto(subscription.SubscriptionType))),
        Problem);
  }   

  [HttpGet("{subscriptionId:Guid}")]

  public async Task<IActionResult> GetSubscription(Guid subscriptionId)
  {
      var query = new GetSubscriptionQuery(subscriptionId);

      var getSubscriptionResult = await sender.Send(query);

      return getSubscriptionResult.MatchFirst(
        subscription => Ok(new SubscriptionResponse(
          subscription.Id,
         ToDto(subscription.SubscriptionType))),
         Problem);
  }

    [HttpDelete("{subscriptionId:guid}")]
  public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
  {
      var command = new DeleteSubscriptionCommand(subscriptionId);

      var createSubscriptionResult = await sender.Send(command);

      return createSubscriptionResult.Match(
          _ => NoContent(),
          Problem);
  }

  private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
{
    return subscriptionType.Name switch
    {
        nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
        nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
        nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
        _ => throw new InvalidOperationException(),
    };
}
}