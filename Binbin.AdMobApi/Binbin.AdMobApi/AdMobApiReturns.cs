using System.Collections.Generic;

namespace Binbin.AdMobApi
{
    public class AdMobApiReturns<TData>
        where TData : class
    {
        public AdMobApiReturns()
        {
            this.errors = new List<string>();
            this.warnings = new List<string>();
            this.data = new List<TData>();
            this.page = new AdMobApiPage();

        }
        public List<string> errors { get; set; }
        public List<string> warnings { get; set; }
        public List<TData> data { get; set; }
        public AdMobApiPage page { get; set; }
    }
}