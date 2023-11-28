using System;
namespace because.Models
{
    public readonly record struct TabbarItem(string Icon, Action OnTap);
}

