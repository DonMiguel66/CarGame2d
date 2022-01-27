using UnityEngine;
using Object = UnityEngine.Object;


namespace CarGame2D
{
    public class BombAbility : IAbility
    {
        private readonly AbilityItemConfig _abilityCfg;

        public BombAbility(AbilityItemConfig config)
        {
            _abilityCfg = config;
        }

        public void Apply()
        {
            var bomb = Object.Instantiate(_abilityCfg.View);
            var rigidBody = bomb.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(Vector2.right * _abilityCfg.Value, ForceMode2D.Impulse);
        }
    }
}
