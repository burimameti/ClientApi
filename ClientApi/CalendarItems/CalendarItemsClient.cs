using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofamilies.ClientApi.Activities;
using Cofamilies.ClientApi.Values;
using Cofamilies.J.Core.CalendarItems;
using Cofamilies.J.Core.Values;
using Rob.Core;

namespace Cofamilies.ClientApi.CalendarItems
{
  public interface ICalendarItemsClient
  {
  }

  public class CalendarItemsClient : ICalendarItemsClient
  {
    // Constructors

    #region CalendarItemsClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    public CalendarItemsClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    {
      MappingEngine = mappingEngine;
      Settings = settings ?? ApiClientSettings.Default;
    }
    #endregion

    // Properties

    public IMappingEngine MappingEngine { get; private set; }
    public IApiClientSettings Settings { get; private set; }

    #region Endpoint
    public string Endpoint
    {
      get { return Settings.CalendarItemsEndpoint; }
    } 
    #endregion

    // Methods

    #region Create(...)
    public  ICalendarItemCreateResult Create(string summary,
      DateTime startDate,
      DateTime endDate,
      DateTime startDateTime,
      DateTime endDateTime,
      string description,
      string eventId,
      List<PersonEvent> people,
      double lattitude = 0.0,
      double longitude = 0.0,
      string locationText = "",
      string recurrenceRule = "",
      string range = null,
      bool isAllDay = false,
      string activityId = null)
    {
      var task = CreateAsync(summary, startDate, endDate, startDateTime, endDateTime, description, eventId, people,
        lattitude, longitude, locationText, recurrenceRule, range, isAllDay, activityId);
      return task.GetAwaiter().GetResult();
    }
    #endregion

    #region CreateAsync(...)
    public async Task<ICalendarItemCreateResult> CreateAsync(string summary,
                                                             DateTime startDate,
                                                             DateTime endDate,
                                                             DateTime startDateTime,
                                                             DateTime endDateTime,
                                                             string description,
                                                             string eventId,
                                                             List<PersonEvent> people, 
                                                             double lattitude = 0.0,
                                                             double longitude = 0.0,
                                                             string locationText = "",
                                                             string recurrenceRule = "",
                                                             string range = null,
                                                             bool isAllDay = false,
                                                             string activityId = null)
    {
      // Create json to post

      var jitem = new JCalendarItemCreate()
      {
        Summary = summary ?? "",
        ActivityId = activityId ?? "",
        StartDate = startDate.ToISO8601(),
        EndDate = endDate.ToISO8601(),
        StartDateTime = startDateTime.ToISO8601(),
        EndDateTime = endDateTime.ToISO8601(),
        Description = description,
        EventId = eventId,
        IsAllDay = isAllDay,
        Range = range,
        Location = new JLocation() { Lattitude = lattitude, Longitude = longitude},
        RecurrenceRule = recurrenceRule,
        LocationText = "",
        People = people.Select(person => MappingEngine.Map<JEventPerson>(person)).ToList()
      };

      // Post

      using (var client = Settings.HttpClientFactory.Create())
      {
        HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, jitem);
        response.EnsureSuccessStatusCode();

        // Read response
        
        var jresult = await response.Content.ReadAsAsync<JCalendarItemCreateResult>();

        // Map

        return MappingEngine.Map<CalendarItemCreateResult>(jresult);
      }
    }
    #endregion
  }
}
