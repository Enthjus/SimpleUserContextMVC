namespace SimpleUser.API.Validators
{
    public interface IBaseValidationModel
    {
        public void Validate(object validator, IBaseValidationModel modelObj);
    }
}
