namespace PatPortal.Domain.Enums
{
    public enum DataAccess
    {
        Undefined = 0,
        Public = 1, //Anybody who entered profile
        Private = 2, //Only for user
        Friends = 3 //Only my firends
    }
}
