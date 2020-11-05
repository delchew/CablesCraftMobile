namespace CablesCraftMobile
{
    public class ModesManager<T> where T : class, new()
    {
        private readonly T mode;
        private readonly JsonRepository repository;
        private readonly string fileName;

        public ModesManager(T mode)
        {
            this.mode = mode;
            repository = new JsonRepository();
            var name = mode.GetType().Name;
            fileName = $"{name}.json";
        }

        public void SaveMode<T>()
        {
            repository.SaveObject(mode, fileName);
        }

        public bool TryLoadMode(out T loadedMode)
        {
            try
            {
                loadedMode = repository.LoadObject<T>(fileName);
            }
            catch
            {
                loadedMode = null;
                return false;
            }
            return true;
        }
    }
}
