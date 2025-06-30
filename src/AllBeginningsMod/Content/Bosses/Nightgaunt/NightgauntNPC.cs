using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.Bosses.Nightgaunt;

public class NightgauntNPC : ModNPC {
    public override string Texture => Assets.Assets.Textures.Bosses.Nightgaunt.KEY_NightgauntBody;

    public NightgauntLimb LeftArm;
    public NightgauntLimb RightArm;
    public NightgauntLimb Torso;

    internal Vector2 _solvePoint;

    public override void SetDefaults() {
        NPC.width = 150;
        NPC.height = 175;
        NPC.knockBackResist = 0f;

        NPC.defense = 10;
        NPC.damage = 60;
        NPC.lifeMax = 999999999;

        NPC.boss = true;
        NPC.noGravity = true;
        NPC.noTileCollide = false;
        NPC.friendly = false;

        NPC.alpha = 0;
        NPC.aiStyle = -1;

        NPC.npcSlots = 40f;
        NPC.HitSound = SoundID.NPCHit2;
    }

    public override void OnSpawn(IEntitySource source) {
        _solvePoint = NPC.Center;
        LeftArm = new NightgauntLimb(NPC.Center + new Vector2(18, 34));
        RightArm = new NightgauntLimb(NPC.Center + new Vector2(-18, 34));
        Torso = new NightgauntLimb(NPC.Center);
        
        var tex = Assets.Assets.Textures.Bosses.Nightgaunt.NightgauntBody.Value;

        var leftUpperArm = new BoneTexture(tex, new Rectangle(0, 104, 32, 16), BoneOrientation.Horizontal);
        var leftLowerArm = new BoneTexture(tex, new Rectangle(34, 100, 44, 22), BoneOrientation.Horizontal);
        var leftHand = new BoneTexture(tex, new Rectangle(86, 96, 32, 28), BoneOrientation.Horizontal);
        
        LeftArm.AddBone(22, leftUpperArm);
        LeftArm.AddBone(44f, leftLowerArm);
        LeftArm.AddBone(32f, leftHand);
        
        RightArm.AddBone(22, leftUpperArm);
        RightArm.AddBone(44f, leftLowerArm);
        RightArm.AddBone(32f, leftHand);

        var neck = new BoneTexture(tex, new Rectangle(48, 0, 24, 26), BoneOrientation.Vertical);
        var body = new BoneTexture(tex, new Rectangle(38, 28, 44, 56), BoneOrientation.Vertical);
        
        Torso.AddBone(22, neck);
        Torso.AddBone(44, body);
    }

    public override void AI() {

        var state = Keyboard.GetState();
        
        if(state.IsKeyDown(Keys.Up)) {
            _solvePoint.Y -= 1.0f;
        }
        if(state.IsKeyDown(Keys.Down)) {
            _solvePoint.Y += 1.0f;
        }
        if(state.IsKeyDown(Keys.Right)) {
            _solvePoint.X += 1.0f;
        }
        if(state.IsKeyDown(Keys.Left)) {
            _solvePoint.X -= 1.0f;
        }
        
        LeftArm.Solve(Main.MouseWorld);
        RightArm.Solve(_solvePoint);
        Torso.Solve(NPC.Center + new Vector2(0, 100));
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        if(Torso != null) {
            Torso.Draw(spriteBatch);
        }
        
        if(LeftArm != null) {
            LeftArm.Draw(spriteBatch);
        }
        if(RightArm != null) {
            RightArm.Draw(spriteBatch);
        }
        return false;
    }
}