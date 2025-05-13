using MediatR;

namespace BuidingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TReponse> : IRequest<TReponse>
    {

    }
}
