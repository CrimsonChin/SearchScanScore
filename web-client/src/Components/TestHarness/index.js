import React from 'react'
import GameService from '../../Services/GameService'
import GameHarness from './gameHarness'
import GuardHarness from './guardHarness'
import TeamHarness from './teamHarness'
import './harness.css'

class TestHarness extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          isLoaded: false,
          gameId: "UVWMN",
          isActive: false,
          collectableItems: [],
          teams: []
        }
    }

    get = () => {
      console.log("Getting game")
       GameService.get(this.state.gameId).then((data) => {
         console.log("Game loaded")
         this.setState({
           isLoaded: true,
           isActive: data.isActive,
           collectableItems: data.collectableItems,
           guards: data.guards, 
           teams: data.teams 
         })
        })
    }

    render() {
      return (
        <div className="harness">
            <h1>Test Harness</h1>
            <input type="text" defaultValue={this.state.gameId}></input>
            <button onClick={this.get}>Load</button>
            { this.state.isLoaded && (
              <>
                <GameHarness gameId={this.state.gameId} isActive={this.state.isActive} collectableItems={this.state.collectableItems}></GameHarness>
                <div>
                  <h2>Guards</h2>
                  {this.state.guards.map((item, i) => 
                    <GuardHarness key={i} guardName={item.name} gameId={this.state.gameId} guardId={item.externalId}></GuardHarness>
                  )}
                </div>
                <div>
                  <h2>Teams</h2>
                  {this.state.teams.map((item, i) => 
                    <TeamHarness key={i} teamName={item.name} gameId={this.state.gameId} teamId={item.externalId}></TeamHarness>
                  )}
                </div>
              </>
            )}

        </div>
      );
    }
  }

  export default TestHarness
  