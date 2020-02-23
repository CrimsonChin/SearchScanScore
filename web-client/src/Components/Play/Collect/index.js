import React from 'react'
import ActiveGameService from '../../../Services/ActiveGameService'

class Collect extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      collectableExternalId: "",
      message: "",
      totalPoints: 0
    }
}

  handleChange = (event) => {
    this.setState({collectableExternalId: event.target.value});
  }

    render() {
      return (
        <div>
          Collect Item:<br/>
          <input type="text" name="collectableExternalId" value={this.state.collectableExternalId} onChange={this.handleChange} /><br/>
          <button onClick={() =>{
                  ActiveGameService.collectItem("UVWMN", "VJG4Q", this.state.collectableExternalId)
                  .then((result) => {
                    if (result.WasSuccess){
                      this.props.onSuccessfulDiscovery(this.state.collectableExternalId)

                      this.setState({
                        collectableExternalId: "",
                        message:"Item successfully collected!",
                        totalPoints: result.Points
                      })

                     return; 
                    }

                    this.setState({collectableExternalId: "",
                    message:"Item could not be collected."})
                  })    

          }}>Score It!</button><br/>
          <p>{this.state.message}</p>
          <p>Items Found: <b>{this.state.totalPoints}</b></p>
        </div>
      );
    }
}

export default Collect
  