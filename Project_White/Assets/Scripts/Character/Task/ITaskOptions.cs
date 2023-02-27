using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Creature;

namespace RPG.Task {
    public interface ITaskOptions {

        public string TaskPath { get; }

        public IEnumerable<ITask> GetTaskOptions(Character askingCharacter);

    }
}