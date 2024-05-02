

namespace Zer0Tools.NotesWebAPI.API
{
    public interface IAPI
    {
        public void Register(WebApplication app);
    }
    public abstract class API
    {
        public abstract void Register(WebApplication app);
    }
}