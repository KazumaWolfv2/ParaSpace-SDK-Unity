﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class RuleHumanBone : RuleBase
{
	// Token: 0x060000CB RID: 203 RVA: 0x00007AE8 File Offset: 0x00005CE8
	public RuleHumanBone()
	{
		this.requiredBones.Add(HumanBodyBones.Hips);
		this.requiredBones.Add(HumanBodyBones.Spine);
		this.requiredBones.Add(HumanBodyBones.LeftUpperArm);
		this.requiredBones.Add(HumanBodyBones.LeftLowerArm);
		this.requiredBones.Add(HumanBodyBones.LeftHand);
		this.requiredBones.Add(HumanBodyBones.LeftUpperLeg);
		this.requiredBones.Add(HumanBodyBones.LeftLowerLeg);
		this.requiredBones.Add(HumanBodyBones.LeftFoot);
		this.requiredBones.Add(HumanBodyBones.RightUpperArm);
		this.requiredBones.Add(HumanBodyBones.RightLowerArm);
		this.requiredBones.Add(HumanBodyBones.RightHand);
		this.requiredBones.Add(HumanBodyBones.RightUpperLeg);
		this.requiredBones.Add(HumanBodyBones.RightLowerLeg);
		this.requiredBones.Add(HumanBodyBones.RightFoot);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00007BBF File Offset: 0x00005DBF
	public override RuleEnumType GetRuleType()
	{
		return RuleEnumType.RuleEnumType_HumanBone;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00007BC3 File Offset: 0x00005DC3
	public override void Reset()
	{
		base.Reset();
		this.missingBones.Clear();
		this.missingAvatar = false;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00007BE0 File Offset: 0x00005DE0
	public override void Check(GameObject root)
	{
		Animator component = root.GetComponent<Animator>();
		if (!component || !component.isHuman)
		{
			this.missingAvatar = true;
			return;
		}
		foreach (HumanBodyBones humanBodyBones in this.requiredBones)
		{
			if (component.GetBoneTransform(humanBodyBones) == null)
			{
				this.missingBones.Add(humanBodyBones);
			}
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00007C68 File Offset: 0x00005E68
	public override void CalResult()
	{
		this.result = RuleResultType.RuleResultType_Prefect;
		if (this.missingAvatar || this.missingBones.Count > 0)
		{
			this.result = RuleResultType.RuleResultType_Bad;
			base.AddRuleFix(RuleErrorType.RuleErrorType_Error, RuleErrorRuleType.RuleErrorRuleType_MissingHumanBone, null, "");
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00007C9D File Offset: 0x00005E9D
	public override RuleResultType GetResult()
	{
		return this.result;
	}

	// Token: 0x040000B8 RID: 184
	private HashSet<HumanBodyBones> missingBones = new HashSet<HumanBodyBones>();

	// Token: 0x040000B9 RID: 185
	private List<HumanBodyBones> requiredBones = new List<HumanBodyBones>();

	// Token: 0x040000BA RID: 186
	private bool missingAvatar;

	// Token: 0x040000BB RID: 187
	private RuleResultType result;
}
