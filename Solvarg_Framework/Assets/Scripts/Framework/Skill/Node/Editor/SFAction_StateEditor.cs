﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using SolvargSkill;
using XMLib;
using UnityEditor;

namespace SolvargSkillEditor
{
    [CustomNodeEditor(typeof(SFAction_StateNode))]
    public class SFAction_StateEditor : NodeEditor
    {
        SFAction_StateNode _target;
        
        public override void OnHeaderGUI()
        {
            GUILayout.Label("状态", NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }

        public override void OnBodyGUI()
        {
            //base.OnBodyGUI();
            _target = target as SFAction_StateNode;
            serializedObject.Update();
            NodeEditorGUILayout.PortField(target.GetInputPort("input")) ;
            _target.stateName = EditorGUILayoutEx.DrawObject("状态名", _target.stateName);
            NodeEditorGUILayout.PortField(new GUIContent("行为"), target.GetOutputPort("output"));
            _target.animNames = EditorGUILayoutEx.DrawObject("动画名", _target.animNames);

            _target.fadeTime = EditorGUILayoutEx.DrawObject("过渡时间", _target.fadeTime);

            _target.enableLoop = EditorGUILayoutEx.DrawObject("循环", _target.enableLoop);
            if (!_target.enableLoop)
            {
                _target.nextStateName = EditorGUILayoutEx.DrawObject("下一个状态", _target.nextStateName);
                _target.nextAnimIndex = EditorGUILayoutEx.DrawObject("下一个状态动画序号", _target.nextAnimIndex);
            }


            serializedObject.ApplyModifiedProperties();
        }

    }
}