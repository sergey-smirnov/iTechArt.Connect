namespace itechart.connect.api.smg.Model.Parameters
{
    public interface IBaseApiParameters<out TInput>
    {
        TInput ToInput();
    }
}