using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoRImmersiveEroMod
{
    public class GameplayInfo
    {
        public bool Update(PlayerStatus player, EnemyDate enemy)
        {
            if (player == null || enemy == null) 
            {   
                return false;
            }
            mPlayerStatus = player;
            mEnemyDate = enemy;
            // Get UI
            if (mPlayerUI == null) 
            { 
            global::UnityEngine.GameObject obj_playerUI = global::UnityEngine.GameObject.Find("UI");
            mPlayerUI = obj_playerUI.GetComponent<global::UImng>();
            }
            // Get playercon 
            if (mPlayercon == null)
            {
                mPlayercon = enemy.com_player;
            }
            // Get Player
            if (mPlayer == null) 
            {
                mPlayer = global::Rewired.ReInput.players.GetPlayer(mPlayercon.playerId);
            }

            // Player stats
            mPlayerMaxHp = player.AllMaxHP();
            mPlayerMaxSp = player.AllMaxSP();
            mPlayerMaxMp = player.AllMaxMP();
            mPlayerHp =  player.Hp;
            mPlayerSp = player.Sp;
            mPlayerMp = player.Mp;
            // Max pleasure will give zero; Min pleasure will give one
            mPlayerPleasure =  1f - (mPlayerStatus._BadstatusVal[0] / 100f);
            mPlayerTotalMaxSpHp = mPlayerStatus.AllMaxSP() + mPlayerStatus.AllMaxHP();
            mPlayerCurrentSpHp = mPlayerStatus.Sp + mPlayerStatus.Hp;
            mPlayerTotalMaxStats = (mPlayerStatus.AllMaxHP() * HpValuability) + (mPlayerStatus.AllMaxSP() * SpValuability) + (mPlayerStatus.AllMaxMP() * MpValuability);
            mPlayerCurrentStats = (mPlayerStatus.Hp * HpValuability) + (mPlayerStatus.Sp * SpValuability) + (mPlayerStatus.Mp * MpValuability);
            mPlayerTotalStats = mPlayerTotalMaxStats + mPlayerCurrentStats;
            mPlayerLostStats = 1f - (mPlayerCurrentStats / mPlayerTotalMaxStats);

            // Player condition 
            mIsPlayerOnGuard = mPlayer.GetButton("Guard");
            mIsPlayerAttack = mPlayer.GetButtonDown("Attack");
            mIsSubmitKeyPressed = mPlayer.GetButtonDown("Submit");
            // Check if player was damaged using  hp revenge damage
            mIsRevengeDamageBar = mPlayerUI.hpbarback.fillAmount > player.Hp / player.AllMaxHP() + 0.01f;
            // Trigger when player gets damage 
            mIsDamageStatus = mPlayercon.state == playercon.DAMAGE;
           
            // Enemy stats
            mEnemyMaxHp = enemy.MaxHp;
            mEnemyMaxSp = enemy.MaxSp;
            mEnemyMaxMp = enemy.MaxMp;
            mEnemyHp = enemy.Hp;
            mEnemySp = enemy.Sp;
            mEnemyMp = enemy.Mp;
            mEnemyTotalMaxStats = (mEnemyDate.MaxHp * HpValuability) + (mEnemyDate.MaxSp * SpValuability) + (mEnemyDate.MaxMp * MpValuability)
                + (mEnemyDate.MaxMp * MpValuability) + mPlayerPleasure;
            mEnemyCurrentStats = (mEnemyDate.Hp * HpValuability) + (mEnemyDate.Sp * SpValuability) + (mEnemyDate.Mp * MpValuability);
            mEnemyTotalStats = mEnemyTotalMaxStats + mEnemyCurrentStats;
            

            // Enemy condition
            mIsEnemyClose = mEnemyDate.distance < 1.2f && mEnemyDate.distance > -1.2f;

            // Weak condition
            mIsPlayerWeak = mPlayerCurrentStats - mPlayerPleasure < mPlayerTotalMaxStats / (6f / mEnemyAdvantage);
            mIsEnemyWeak = mEnemyCurrentStats < mEnemyTotalMaxStats / (6f / mPlayerAdvantage);

            // Stronger condition
            mIsPlayerStronger = mPlayerTotalStats * 0.7f > mEnemyTotalStats;
            mIsEnemyStronger = mEnemyTotalStats * 0.7f > mPlayerTotalStats;

            // Advantage condition
            mPlayerAdvantage = mPlayerTotalStats / mEnemyTotalStats;
            mEnemyAdvantage = mEnemyTotalStats / mPlayerTotalStats;








            float SpDamageMultiplier = mEnemyDate.Sp * 0.1f * Time.deltaTime;
            float SpDamageToEnemy = mPlayerStatus.Sp * SpDamageMultiplier;
            float SpDamageToPlayer = mEnemyDate.Sp * SpDamageMultiplier;
            bool EnemyHaveSt = mEnemySp > SpDamageToEnemy;
            bool PlayerHaveSt = mPlayerStatus.Sp > SpDamageToPlayer;
            bool CanEscape = mPlayerStatus.Sp > mPlayerStatus.AllMaxSP() * 0.99;

            Plugin.LoggerMessage01 = "Player state " + mPlayercon.state;
            Plugin.LoggerMessage02 = "EnemyHaveSt = " + EnemyHaveSt;
            //Plugin.LoggerMessage03 = "mEnemyTotalStats = " + mEnemyTotalStats;
            //Plugin.LoggerMessage04 = "mPlayerTotalStats = " + mPlayerTotalStats;


            return true;
        }

        public bool Update()
        {
            return Update(mPlayerStatus, mEnemyDate);
        }

        public bool Update(PlayerStatus player)
        {
            return Update(mPlayerStatus, mEnemyDate);
        }

        public float BuffPlayer(float cof) 
        {
            // Bonuses and debufs
            bool buf_01 = mIsPlayerStronger && mIsEnemyWeak;
            bool buf_02 = mIsPlayerStronger;
            bool buf_03 = mIsEnemyWeak;

            bool debuf_01 = mIsEnemyStronger && mIsPlayerWeak;
            bool debuf_02 = mIsEnemyStronger;
            bool debuf_03 = mIsPlayerWeak;

            if (buf_01)
            {
                cof *= BuffStrength;
            }
            if (buf_02)
            {
                cof *= BuffStrength;
            }
            if (buf_03)
            {
                cof *= BuffStrength;
            }
            if (debuf_01)
            {
                cof /= (BuffStrength);
            }
            if (debuf_02)
            {
                cof /= (BuffStrength);
            }
            if (debuf_03)
            {
                cof /= (BuffStrength); ;
            }
            return cof;
        }

        public float BuffEnemy(float cof)
        {
            // Bonuses and debufs
            bool buf_01 = mIsPlayerStronger && mIsEnemyWeak;
            bool buf_02 = mIsPlayerStronger;
            bool buf_03 = mIsEnemyWeak;

            bool debuf_01 = mIsEnemyStronger && mIsPlayerWeak;
            bool debuf_02 = mIsEnemyStronger;
            bool debuf_03 = mIsPlayerWeak;

            if (buf_01)
            {
                cof /= BuffStrength;
            }
            if (buf_02)
            {
                cof /= BuffStrength;
            }
            if (buf_03)
            {
                cof /= BuffStrength;
            }
            if (debuf_01)
            {
                cof *= (BuffStrength);
            }
            if (debuf_02)
            {
                cof *= (BuffStrength);
            }
            if (debuf_03)
            {
                cof *= (BuffStrength); ;
            }
            return cof;
        }

        public static float RegenerationFromSource(float source)
        {
            float Regeneration = (float)(0.2f * global::System.Math.Log(0.2 * source + 1.0) *
                (1.0 * global::System.Math.Pow(source, 0.5) + 2.71828182846f) + -1.9 * global::System.Math.Pow(1.0, source) + 1.9) / 20f;
            return Regeneration;
        }

        public GameplayInfo()
        {
        }

        public static PlayerStatus mPlayerStatus = null;
        public static EnemyDate mEnemyDate = null;
        public static global::Rewired.Player mPlayer = null;
        public static playercon mPlayercon = null;

        // Player stats
        public float mPlayerMaxHp;
        public float mPlayerMaxSp;
        public float mPlayerMaxMp;
        public float mPlayerHp;
        public float mPlayerSp;
        public float mPlayerMp;
        public float mPlayerTotalMaxStats;
        public float mPlayerCurrentStats;
        public float mPlayerPleasure;
        public float mPlayerTotalMaxSpHp;
        public float mPlayerCurrentSpHp;
        public float PlayerTotalStats;
        public float mPlayerTotalStats;
        public float mPlayerAdvantage;
        // 1 - (Stats/MaxStats)
        public float mPlayerLostStats; 

        // Player condition 
        public bool mIsRevengeDamageBar;
        public bool mIsDamageStatus;
        public bool mIsPlayerOnGuard;
        public bool mIsPlayerAttack;
        public bool mIsPlayerWeak;
        public bool mIsSubmitKeyPressed;
        public bool mIsPlayerStronger;

        // Enemy stats
        public float mEnemyMaxHp;
        public float mEnemyMaxSp;
        public float mEnemyMaxMp;
        public float mEnemyHp;
        public float mEnemySp;
        public float mEnemyMp;
        public float mEnemyTotalMaxStats;
        public float mEnemyCurrentStats;
        public float mEnemyTotalStats;
        public float mEnemyAdvantage;

        // Enemy condition 
        public bool mIsEnemyWeak; 
        public bool mIsEnemyStronger;
        public bool mIsEnemyClose;
        // Other
        private global::UImng mPlayerUI = null;

        // Valuability coeficient every stats have value
        public const float HpValuability = 8f;
        public const float SpValuability = 6f;
        public const float MpValuability = 2.5f;
        public const float SpRegenWhenDowned = 1;
        public const float BuffStrength = 1.2f;
        // From 0..1 where 1 mPlayerMaxSp
        public const float RapeEscapeThreshold = 0.95f;
    }   
}
