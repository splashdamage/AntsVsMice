using UnityEngine;
using System.Collections;

public class Sprayer: Tower {
	public override void Reload(Ammo newAmmo) {}
	public override void Launch(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum) {
		base.Reload(null);
		base.Launch(sprite, clip, frame, frameNum);
	}
}
