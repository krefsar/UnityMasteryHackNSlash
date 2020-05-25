public interface IAttack : IDamage
{
    bool CanAttack { get; }

    void Attack();
}