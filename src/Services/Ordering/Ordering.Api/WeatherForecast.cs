namespace Ordering.Api
{
    /// <summary>
    /// 天气预报实体类
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// 其他温度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// 天气概要
        /// </summary>
        public string? Summary { get; set; }
    }
}