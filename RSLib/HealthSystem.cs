﻿namespace RSLib
{
    using RSLib.Maths;

    /// <summary>
    /// Class used to manage a health system. Every living unit can have an instance on this class and listen to the Killed event to be notified when dead.
    /// Methods of this class should be accessed by some methods implemented by an interface (something like ILivingUnit) so that there can be a clean
    /// way to handle additional conditions, heal or damage sources, etc.
    /// </summary>
    public class HealthSystem
    {
        #region FIELDS

        private int health;

        #endregion FIELDS

        #region CONSTRUCTORS

        public HealthSystem(int initHealth)
        {
            this.MaxHealth = initHealth;
            this.Health = initHealth;
        }

        #endregion CONSTRUCTORS

        #region EVENTS

        public delegate void KilledEventHandler();

        public event KilledEventHandler Killed;

        #endregion EVENTS

        #region PROPERTIES

        public int Health
        {
            get => this.health;
            set
            {
                this.health = value.Clamp(0, this.MaxHealth);
                if (this.IsDead)
                {
                    this.Killed?.Invoke();
                }
            }
        }

        /// <summary>Current health percentage as a value from 0 to 1.</summary>
        public float HealthPercentage => (float)this.Health / this.MaxHealth;

        public bool IsDead => this.Health == 0;

        public int MaxHealth { get; private set; }

        #endregion PROPERTIES

        #region METHODS

        /// <summary>Instantly changes the maximum health. Health is reduced if new maximum health is less than health value.</summary>
        /// <param name="newValue">New maximum health value.</param>
        /// <param name="increaseHealth">Does health also increase if new maximum health is higher than its previous value.</param>
        public void ChangeMaxHealth(int newValue, bool increaseHealth = true)
        {
            int previousMaxHealth = this.MaxHealth;
            this.MaxHealth = newValue;

            if (this.MaxHealth > previousMaxHealth && increaseHealth)
            {
                this.Health += this.MaxHealth - previousMaxHealth;
            }
            else if (this.MaxHealth < this.Health)
            {
                this.Health = this.MaxHealth;
            }
        }

        /// <summary>Removes a given amount of health points.</summary>
        /// <param name="amount">Amount to remove.</param>
        public void Damage(int amount)
        {
            this.Health -= amount;
        }

        /// <summary>Restores a given amount of health points.</summary>
        /// <param name="amount">Amount to restore.</param>
        /// <param name="ignoreIfDead">If true, heal is not applied if current health is equal to 0.</param>
        public void Heal(int amount, bool ignoreIfDead = true)
        {
            if (this.IsDead && ignoreIfDead)
            {
                return;
            }

            this.Health += amount;
        }

        /// <summary>Sets health value to maximum health value.</summary>
        /// <param name="ignoreIfDead">If true, heal is not applied if current health is equal to 0.</param>
        public void HealFull(bool ignoreIfDead = true)
        {
            if (this.IsDead && ignoreIfDead)
            {
                return;
            }

            this.Health = this.MaxHealth;
        }

        /// <summary>Sets health value to 0, and then kills the unit and triggers the Killed event.</summary>
        public void Kill()
        {
            if (this.IsDead)
            {
                UnityEngine.Debug.LogWarning("LivingUnit.Kill() WARNING: Can not kill an already dead unit, aborting.");
                return;
            }

            this.Health = 0;
        }

        #endregion METHODS
    }
}