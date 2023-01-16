using Newtonsoft.Json;
using System.Collections.Generic;

namespace CourseManager.Models.Dtos
{
  public class PaginatedResultsDto<T> where T : class
  {
    [JsonProperty(Order = 1)]
    public int Page { get; set; }
    [JsonProperty(Order = 2)]
    public int PageSize { get; set; }
    [JsonProperty(Order = 3)]
    public int TotalCount { get; set; }
    [JsonProperty(Order = 4)]
    public ICollection<T> Values { get; set; }

    public PaginatedResultsDto()
    {
      Values = new List<T>();
    }
  }
}
