// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Update.Internal
{
    public class ModelDataTracker : IModelDataTracker
    {
        private readonly IStateManager _stateManager;
        private readonly IChangeDetector _changeDetector;

        public ModelDataTracker(
            [NotNull] IStateManager stateManager,
            [NotNull] IChangeDetector changeDetector)
        {
            _stateManager = stateManager;
            _changeDetector = changeDetector;
        }

        public IUpdateEntry GetPrincipal(IUpdateEntry dependentEntry, IForeignKey foreignKey)
            => _stateManager.GetPrincipal((InternalEntityEntry)dependentEntry, foreignKey);

        public IEnumerable<IUpdateEntry> GetDependents(IUpdateEntry principalEntry, IForeignKey foreignKey)
            => _stateManager.GetDependents((InternalEntityEntry)principalEntry, foreignKey);

        public IUpdateEntry TryGetEntry(IKey key, object[] keyValues)
            => _stateManager.TryGetEntry(key, keyValues);

        public IEnumerable<IUpdateEntry> Entries
            => _stateManager.Entries;

        public void DetectChanges()
            => _changeDetector.DetectChanges(_stateManager);

        public IList<IUpdateEntry> GetEntriesToSave()
            => _stateManager.GetEntriesToSave();

        public IUpdateEntry CreateEntry(
            IDictionary<string, object> values,
            IEntityType entityType)
            => _stateManager.CreateEntry(values, entityType);
    }
}
