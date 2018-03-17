using strange.extensions.context.api;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MVCSContext {
    public GameContext(MonoBehaviour view, bool autoMapping) : base(view, autoMapping)
	{

    }


    protected override void mapBindings()
    {
        //injection
        injectionBinder.Bind<ISomeService>().To<GameService>().ToSingleton();
        injectionBinder.Bind<GameModel>().To<GameModel>().ToSingleton();

        //command
        commandBinder.Bind(GameEvents.CommandEvnt.REQUEST_MAPINFO).To<RequestMapInfoCommand>();
        commandBinder.Bind(GameEvents.CommandEvnt.UPDATE_SHow).To<UpdateShowCommand>();
        commandBinder.Bind(GameEvents.CommandEvnt.START_GAME).To<StartGameCommand>();
        commandBinder.Bind(GameEvents.CommandEvnt.GAME_OVER).To<GameOverCommand>();
        commandBinder.Bind(GameEvents.CommandEvnt.CREATE_TOKEN).To<CreateTokenCommand>();
        commandBinder.Bind(GameEvents.CommandEvnt.DESTROY_TOKEN).To<DestroyTokenCommand>();
        commandBinder.Bind(GameEvents.CommandEvnt.STOP_GAME).To<StopGameCommand>();


        //mediater
        mediationBinder.Bind<WelcomeView>().To<WelcomeMediator>();
        mediationBinder.Bind<SelectMapView>().To<SelectMapMediator>();
        mediationBinder.Bind<PlayingView>().To<PlayingMediator>();
        mediationBinder.Bind<WinView>().To<WinMediator>();
        mediationBinder.Bind<LooseView>().To<LooseMediator>();
        mediationBinder.Bind<SpawnerView>().To<SpawnerViewMediator>();
        mediationBinder.Bind<TouchView>().To<TouchViewMediator>();


        commandBinder.Bind(ContextEvent.START).To<FirstCommand>().Once();
    }
}
