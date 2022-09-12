using System;

namespace Tourist.Application.Commands;
public interface ICommand
{
    public Guid? CommandId => Guid.NewGuid();
}