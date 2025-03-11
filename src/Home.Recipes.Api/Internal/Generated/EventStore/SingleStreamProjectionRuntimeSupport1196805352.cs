// <auto-generated/>
#pragma warning disable
using Marten;
using Marten.Events.Aggregation;
using Marten.Internal.Storage;
using System;
using System.Linq;

namespace Marten.Generated.EventStore
{
    // START: SingleStreamProjectionLiveAggregation1196805352
    public class SingleStreamProjectionLiveAggregation1196805352 : Marten.Events.Aggregation.SyncLiveAggregatorBase<Home.Recipes.Domain.Recipes.Recipe>
    {
        private readonly Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.Recipes.Recipe> _singleStreamProjection;

        public SingleStreamProjectionLiveAggregation1196805352(Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.Recipes.Recipe> singleStreamProjection)
        {
            _singleStreamProjection = singleStreamProjection;
        }



        public override Home.Recipes.Domain.Recipes.Recipe Build(System.Collections.Generic.IReadOnlyList<Marten.Events.IEvent> events, Marten.IQuerySession session, Home.Recipes.Domain.Recipes.Recipe snapshot)
        {
            if (!events.Any()) return snapshot;
            var usedEventOnCreate = snapshot is null;
            snapshot ??= Create(events[0], session);;
            if (snapshot is null)
            {
                usedEventOnCreate = false;
                snapshot = CreateDefault(events[0]);
                if (snapshot != null) _singleStreamProjection.ApplyMetadata(snapshot, events[0]);
            }

            foreach (var @event in events.Skip(usedEventOnCreate ? 1 : 0))
            {
                snapshot = Apply(@event, snapshot, session);
                if (snapshot != null) _singleStreamProjection.ApplyMetadata(snapshot, @event);
            }

            return snapshot;
        }


        public Home.Recipes.Domain.Recipes.Recipe Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeCreated> event_RecipeCreated1:
                    return Home.Recipes.Domain.Recipes.Recipe.Create(event_RecipeCreated1.Data);
                    break;
            }

            return null;
        }


        public Home.Recipes.Domain.Recipes.Recipe Apply(Marten.Events.IEvent @event, Home.Recipes.Domain.Recipes.Recipe aggregate, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.CookingTimeAdjusted> event_CookingTimeAdjusted2:
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_CookingTimeAdjusted2.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.FluidIngredientAdded> event_FluidIngredientAdded3:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_FluidIngredientAdded3.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.IngredientChanged> event_IngredientChanged13:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_IngredientChanged13.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.IngredientRemoved> event_IngredientRemoved6:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_IngredientRemoved6.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.MassIngredientAdded> event_MassIngredientAdded4:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_MassIngredientAdded4.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.PieceIngredientAdded> event_PieceIngredientAdded5:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_PieceIngredientAdded5.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.PreparationTimeAdjusted> event_PreparationTimeAdjusted7:
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_PreparationTimeAdjusted7.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeDescriptionChanged> event_RecipeDescriptionChanged8:
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeDescriptionChanged8.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeImageAdded> event_RecipeImageAdded14:
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeImageAdded14.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeNameChanged> event_RecipeNameChanged9:
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeNameChanged9.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeStepUpdated> event_RecipeStepUpdated12:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeStepUpdated12.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.StepAdded> event_StepAdded10:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_StepAdded10.Data, aggregate);
                    break;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.StepRemoved> event_StepRemoved11:
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_StepRemoved11.Data, aggregate);
                    break;
            }

            return aggregate;
        }

    }

    // END: SingleStreamProjectionLiveAggregation1196805352
    
    
    // START: SingleStreamProjectionInlineHandler1196805352
    public class SingleStreamProjectionInlineHandler1196805352 : Marten.Events.Aggregation.AggregationRuntime<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Marten.IDocumentStore _store;
        private readonly Marten.Events.Aggregation.IAggregateProjection _projection;
        private readonly Marten.Events.Aggregation.IEventSlicer<Home.Recipes.Domain.Recipes.Recipe, System.Guid> _slicer;
        private readonly Marten.Internal.Storage.IDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid> _storage;
        private readonly Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.Recipes.Recipe> _singleStreamProjection;

        public SingleStreamProjectionInlineHandler1196805352(Marten.IDocumentStore store, Marten.Events.Aggregation.IAggregateProjection projection, Marten.Events.Aggregation.IEventSlicer<Home.Recipes.Domain.Recipes.Recipe, System.Guid> slicer, Marten.Internal.Storage.IDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid> storage, Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.Recipes.Recipe> singleStreamProjection) : base(store, projection, slicer, storage)
        {
            _store = store;
            _projection = projection;
            _slicer = slicer;
            _storage = storage;
            _singleStreamProjection = singleStreamProjection;
        }



        public override async System.Threading.Tasks.ValueTask<Home.Recipes.Domain.Recipes.Recipe> ApplyEvent(Marten.IQuerySession session, Marten.Events.Projections.EventSlice<Home.Recipes.Domain.Recipes.Recipe, System.Guid> slice, Marten.Events.IEvent evt, Home.Recipes.Domain.Recipes.Recipe aggregate, System.Threading.CancellationToken cancellationToken)
        {
            switch (evt)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.CookingTimeAdjusted> event_CookingTimeAdjusted16:
                    aggregate ??= CreateDefault(evt);
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_CookingTimeAdjusted16.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.FluidIngredientAdded> event_FluidIngredientAdded17:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_FluidIngredientAdded17.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.IngredientChanged> event_IngredientChanged27:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_IngredientChanged27.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.IngredientRemoved> event_IngredientRemoved20:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_IngredientRemoved20.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.MassIngredientAdded> event_MassIngredientAdded18:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_MassIngredientAdded18.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.PieceIngredientAdded> event_PieceIngredientAdded19:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_PieceIngredientAdded19.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.PreparationTimeAdjusted> event_PreparationTimeAdjusted21:
                    aggregate ??= CreateDefault(evt);
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_PreparationTimeAdjusted21.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeCreated> event_RecipeCreated29:
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Create(event_RecipeCreated29.Data);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeDeleted> event_RecipeDeleted30:
                    aggregate ??= CreateDefault(evt);
                    if (aggregate == null) return null;
                    var result_of_ShouldDelete1 = Home.Recipes.Domain.Recipes.Recipe.ShouldDelete(event_RecipeDeleted30.Data, aggregate);
                    if (result_of_ShouldDelete1)
                    {
                        return null;
                    }

                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeDescriptionChanged> event_RecipeDescriptionChanged22:
                    aggregate ??= CreateDefault(evt);
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeDescriptionChanged22.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeImageAdded> event_RecipeImageAdded28:
                    aggregate ??= CreateDefault(evt);
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeImageAdded28.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeNameChanged> event_RecipeNameChanged23:
                    aggregate ??= CreateDefault(evt);
                    aggregate = Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeNameChanged23.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeStepUpdated> event_RecipeStepUpdated26:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_RecipeStepUpdated26.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.StepAdded> event_StepAdded24:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_StepAdded24.Data, aggregate);
                    return aggregate;
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.StepRemoved> event_StepRemoved25:
                    aggregate ??= CreateDefault(evt);
                    Home.Recipes.Domain.Recipes.Recipe.Apply(event_StepRemoved25.Data, aggregate);
                    return aggregate;
            }

            return aggregate;
        }


        public Home.Recipes.Domain.Recipes.Recipe Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.Recipes.Events.RecipeCreated> event_RecipeCreated15:
                    return Home.Recipes.Domain.Recipes.Recipe.Create(event_RecipeCreated15.Data);
                    break;
            }

            return null;
        }

    }

    // END: SingleStreamProjectionInlineHandler1196805352
    
    
}

