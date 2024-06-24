using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TND.Application.Reviews.Common
{
    public record ReviewResponse(
        Guid Id,
         string Content,
         int Rating,
         DateTime CreatedAtUtc,
         DateTime? ModifiedAtUtc,
         string GuestName);

}
