using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TND.Application.Reviews.Common;

namespace TND.Application.Reviews.Update
{
    public record UpdateReviewCommand(
        Guid ReviewId,
        Guid GuestId,
        Guid HotelId,
        string Content,
        int Rating) : IRequest;
}
