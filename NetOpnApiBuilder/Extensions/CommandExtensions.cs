using System.Linq;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.Extensions
{
    public static class CommandExtensions
    {
        public static IQueryable<ApiCommand> NewFromApi(this IQueryable<ApiCommand> self)
            => self.Where(x => x.NewCommand == true);

        public static IQueryable<ApiCommand> ChangedInApi(this IQueryable<ApiCommand> self)
            => self.Where(x => x.CommandChanged == true);

        public static IQueryable<ApiCommand> NewOrChangedInApi(this IQueryable<ApiCommand> self)
            => self.Where(x => x.NewCommand == true || x.CommandChanged == true);
        
        public static IQueryable<ApiCommand> MissingSettings(this IQueryable<ApiCommand> self)
            => self.Where(x => 
                (x.UsePost == true && x.HasNoPostBody == false && x.PostBodyDataType == null)
                || (x.HasNoResponseBody == false && x.ResponseBodyDataType == null)
            );
        
        
    }
}
