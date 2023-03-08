namespace Pizza.Common.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { } 

        public NotFoundException(int key) 
            : base($"Объект с rk.xjv - \"{key}\" не найден!") {}

        public NotFoundException(string name) 
            : base($"Объект с именем - \"{name}\" не найден!") {}

        public NotFoundException(string name, int key) 
            : base($"Объект с именем - \"{name}\" и ключом - ({key}) не найден!") {}

    }
}