// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Update.Internal
{
    public class ModelDataTrackerFactory : IModelDataTrackerFactory
    {
        private readonly StateManagerDependencies _stateManagerDependencies;
        private readonly IChangeDetector _changeDetector;

        public ModelDataTrackerFactory(
            [NotNull] StateManagerDependencies stateManagerDependencies,
            [NotNull] IChangeDetector changeDetector)
        {
            _stateManagerDependencies = stateManagerDependencies;
            _changeDetector = changeDetector;
        }

        public IModelDataTracker Create(IModel model)
            => new ModelDataTracker(
                new StateManager(
                    _stateManagerDependencies.With(model)),
                _changeDetector);
    }
}
