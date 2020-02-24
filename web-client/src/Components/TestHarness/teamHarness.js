import React from 'react'
import TeamService from '../../Services/TeamService'
import Player from './player'

class TeamHarness extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          players: 0
        }
    }

    canJoinTeam = (event) => {
      console.log("Can Join Team?")
      TeamService.canJoinTeam(this.props.gameId, this.props.teamId).then((data) => {
        console.log("Has joined team: " + data)
        this.setState({
          hasJoinedTeam: data,
          players: this.state.players + 1
        })
      })
    }

    players = () => {
      const playerComponents = [];

      for (let i = 0; i < this.state.players; i++) {
        playerComponents.push(<Player key={i} gameId={this.props.gameId} teamId={this.props.teamId} playerNumber={i + 1}></Player>)
      }

      return playerComponents;
    }

    render() {
      return (
          <div className="team">
            <h3>{this.props.teamName} ({this.props.teamId}) <button onClick={this.canJoinTeam}>Add Player</button></h3>
            
            {this.players()}
        </div>
      );
    }
  }

  export default TeamHarness
  