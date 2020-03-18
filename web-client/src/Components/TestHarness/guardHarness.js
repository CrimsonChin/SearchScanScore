import React from 'react'
import GuardService from '../../Services/GuardService'

class GuardHarness extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          teamId: ""
        }
    }

    handleChange = (event) => {
      this.setState({teamId: event.target.value});
    }

    recordSighting = (event) => {
      console.log("Spotted Team: ", this.state.teamId)
      GuardService.recordSighting(this.props.gameId, this.props.guardId, this.state.teamId).then((data) => {
        console.log(data)
        this.setState({
          teamId: ""
        })
      })

      this.setState({
        teamId: ""
      })
    }

    render() {

      return (
        <div className="guard">
          <h3>{this.props.guardName} ({this.props.guardId})</h3>
          <input type="text" value={this.state.teamId} onChange={this.handleChange}></input>
          <button onClick={this.recordSighting}>Record Sighting</button> 
        </div>
      );
    }
  }

  export default GuardHarness
  