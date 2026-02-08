using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Application.Common.Helpers;
namespace HRIS.Application.Features.TimeEntries.Handlers
{
    public class ClockInHandler : IRequestHandler<ClockInCommand, int>
    {
        private readonly ITimeEntryRepository _repo;
        private readonly IPublicIpService _ipaddress;

        public ClockInHandler(ITimeEntryRepository repo, IPublicIpService ipaddress)
        {
            _repo = repo;
            _ipaddress = ipaddress;
        } 

        public async Task<int> Handle(ClockInCommand request, CancellationToken ct)
        {
            var phTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
            var philippineTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, phTimeZone);

    
            var entry = new TimeEntry
            {
                UserId = request.UserId,
                UserName = request.UserName,
                IpAddress = await _ipaddress.GetPublicIpAsync(),
                ClockIn = PhilippineTime.Now
            };

            await _repo.Add(entry);
            return entry.Id;
        }
    }
}
