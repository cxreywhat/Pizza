<h1>
    Данный Api разрабатывается(-лся) в учебных целях.
</h1>
    В данном Api реализованы:</br>
        Контроллер для получения запросов о Пицце и о пользователе.:</br>
        Работа с базой данных при помощи orm EF7 <b>(Data -> DataContext)</b></br>
        Созданы роли пользователей <b>(User, Employee, Manager, Admin, SuperAdmin)</b>,
    каждая из них дает доступ для работы с определленным функционалом сервера.</br>
        Создана <b>авторизация пользователя и аутентификация</b>. Имеется <b>проверка паролей,
    хэширование паролей, получения токена для авторизации в системе, у каждого пользователя имеется: своя роль, список пицц добавленных им(Common-> Authorization -> JwtUtils и Repository -> UserRepository и AuthRepository)</b></br>
        Создан маппинг полей моделей User, Pizza. Имеется класс с кастомными ошибками. Имеется
    кастомный конвейeр для получения Пользователя, чтобы в дальнейшем узнать его роли <b>(Common -> Mapping, Authorization, CustomException)</b></br>
    Контроллер Pizza имеет данный функционал(<b>Controllers->PizzaController</b>):</br>
        1. GetAllPizza - получение всех пицц доступен всем, даже не авторизованным в системе</br>
        <b>2.</b> GetById - получение пиццы по ее id, так же доступна всем</br>
        <b>3.</b> GetByQueryParams - получение пиццы по ее хар-кам, так же доступна всем</br>
        <b>4.</b> CreatePizza - создание пиццы, доступна только Admin и SuperAdmin</br>
        <b>5.</b> UpdatePizza - измение пиццы, доступна только Admin и SuperAdmin</br>
        <b>6.</b> DeletePizza - удаление пиццы по id, доступна только Admin и SuperAdmin</br>
        <b>7.</b> AddPizzaToUser - добавление пиццы по ее id в профиль пользователя, кто добавлял ее. Доступна всем кто авторизован в системе. </br>
    <b>Контроллер User имеет данный функционал (Controllers->UserController</b>):</br>
       <b>1.</b> GetAllUser - получение всех пользователей доступен всем, даже не авторизованным в системе</br>
        <b>2.</b> GetById - получение пользователя по его id, так же доступен всем</br>
        <b>3.</b> CreateUser - создание пользователя, доступна только Admin и SuperAdmin</br>
        <b>4.</b> UpdateUser - измение пользователя, доступна только Admin и SuperAdmin</br>
        <b>5.</b> DeleteUser - удаление пользователя по id, доступна только Admin и SuperAdmin</br>
    Контроллер обращается через интерфейс репозитория, логика контроллера пиццы - скрыта в <b>Repositoy->PizzaRepository</b>, </br>
    Контроллер обращается через интерфейс репозитория, логика контроллера пользователя - в <b>Repositoy->UserRepository</b></br>
    В <b>Repository->AuthRepository</b> скрыта логика для регистрации и авторизации и реализована в контролле <b>Controller->AuthController</b></br>
    PizzaRepository и UserRepository наследуются от обобщенного репозитория GenericRepository куда мы передаем лишь модель возращаемого типо и по которой будет обращаться к бд</br>
