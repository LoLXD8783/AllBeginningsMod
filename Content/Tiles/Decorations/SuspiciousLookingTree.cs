using AllBeginningsMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace AllBeginningsMod.Content.Tiles.Decorations;

public class SuspiciousLookingTree : ModTile {

    public override string Texture => "Terraria/Images/Item_0";

    internal enum TreeState { Normal, Decay, Evil, Dead }

    TreeState state;

    public override void SetStaticDefaults() {
        MinPick = int.MaxValue;
        DustType = DustID.WoodFurniture;

        Main.tileFrameImportant[Type] = false;

        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.Width = 1;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

        TileObjectData.newTile.DrawYOffset = -2;
        TileObjectData.addTile(Type);

        state = TreeState.Normal;

    }

    public override void NearbyEffects(int i, int j, bool closer) {
        var pos = new Vector2(4 + i * 16, 4 + j * 16);

        if(!Main.npc.Any(NPC => NPC.type == NPCType<StrangeTreeNPC>() && (NPC.ModNPC as StrangeTreeNPC).Parent == Main.tile[i, j] && NPC.active)) {
            int tree = NPC.NewNPC(new EntitySource_WorldEvent(), (int)pos.X + 6, (int)pos.Y - 30, NPCType<StrangeTreeNPC>());

            if(Main.npc[tree].ModNPC is StrangeTreeNPC)
                (Main.npc[tree].ModNPC as StrangeTreeNPC).Parent = Main.tile[i, j];
        }
        base.NearbyEffects(i, j, closer);
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
        var tex = Request<Texture2D>("AllBeginningsMod/Content/Tiles/Decorations/SuspiciousLookingTreeStages").Value;

        Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        var offset = new Vector2(30, 116);

        switch(state) {
            case TreeState.Normal:
                spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + zero - offset, new Rectangle(0, 0, 84, 182), Lighting.GetColor(i, j));
                break;
            case TreeState.Decay:
                spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + zero - offset, new Rectangle(84, 0, 84, 182), Lighting.GetColor(i, j));
                break;
            case TreeState.Evil:
                spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + zero - offset, new Rectangle(168, 0, 84, 182), Lighting.GetColor(i, j));
                break;
            case TreeState.Dead:
                spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + zero - offset, new Rectangle(252, 0, 84, 182), Lighting.GetColor(i, j));
                break;
        }

        return false;
    }
}

public class StrangeTreeNPC : ModNPC {
    public override string Texture => "Terraria/Images/Item_0";

    public Tile Parent;

    public override void SetStaticDefaults() {
        Main.npcFrameCount[Type] = 1;

        NPC.Happiness
            .SetBiomeAffection<ForestBiome>(AffectionLevel.Love);

        base.SetStaticDefaults();
    }

    public override void SetDefaults() {
        NPC.width = 16;
        NPC.height = 16;

        NPC.defense = 50;
        NPC.damage = 50;
        NPC.lifeMax = 10000;

        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.friendly = true;
        NPC.townNPC = true;

        NPC.alpha = 0;
        NPC.aiStyle = -1;

        NPC.HitSound = SoundID.Dig;
    }

    public override string GetChat() {
        List<string> dialogues = new();
        for(int i = 0; i < 4; i++)
            dialogues.Add(Language.GetTextValue("Mods.AllBeginningsMod.NPCs.StrangeTreeNPC.Dialogue.Line" + i));

        return Main.rand.Next(dialogues);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            new FlavorTextBestiaryInfoElement("Really cool looking talking tree. Nothing to see here.")
        });
        base.SetBestiary(database, bestiaryEntry);
    }

    private void DrawInBestiary(SpriteBatch spriteBatch, Vector2 screenPos) {
        var tex = Request<Texture2D>("AllBeginningsMod/Assets/Images/Misc/GleefulTreeBestiary").Value;
        NPC.Center += Main.screenPosition - screenPos;

        int currentFrame = (int)(Main.GameUpdateCount / 5) % 7;

        var sourceRect = new Rectangle(0, currentFrame * 192, 102, 192);

        spriteBatch.Draw(tex, NPC.Center - Main.screenPosition, sourceRect, Color.White, 0, sourceRect.Size() / 2, 0.5f, SpriteEffects.None, 0f);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        if(NPC.IsABestiaryIconDummy) {
            DrawInBestiary(spriteBatch, screenPos);
            return false;
        }
        return base.PreDraw(spriteBatch, screenPos, drawColor);
    }
}