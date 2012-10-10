using System.Collections.Generic;

namespace Binbin.AdMobApi
{

    public class AdMobApiReturn<TData>
        where TData : class
    {
        public AdMobApiReturn()
        {
            this.errors = new List<string>();
            this.warnings = new List<string>();
            this.data = default(TData);
            this.page = new AdMobApiPage();

        }
        public List<string> errors { get; set; }
        public List<string> warnings { get; set; }
        public TData data { get; set; }
        public AdMobApiPage page { get; set; }
    }
}