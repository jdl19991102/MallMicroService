namespace Ordering.Api
{
    /// <summary>
    /// ����Ԥ��ʵ����
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// �¶�
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// �����¶�
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// ������Ҫ
        /// </summary>
        public string? Summary { get; set; }
    }
}