import React from 'react'
import Collect from './Collect'
import ActiveGameService from '../../Services/ActiveGameService'

class Home extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          collectables: []
        }
    }

    componentDidMount(){
      this.getCollectables();
    }

    onSuccessfulDiscovery = () => {
      this.getCollectables();
    }

    getCollectables = () => {
      ActiveGameService.getCollectables("UVWMN").then((data) => {
        this.setState({collectables: data})
      })
    }

    render() {
       
      const col = {
        width:"200px",
        heigth:"800px",
        float:"left",
      }

      const col2 = {
        heigth: "800px",
        display:"inline-block"
      }

      return (
        <div>
            <h2>Play</h2>
            <div style={col}>
              <h3>Collect</h3>
              <Collect onSuccessfulDiscovery={this.onSuccessfulDiscovery}></Collect>
            </div>

            <div style={col2}>
              <h3>Clues</h3>
              <ul>
                {this.state.collectables.map((item) => 
                  <li>{item.Name}</li>
                ) }
              </ul>
        </div>


            
        </div>
      );
    }
  }

  export default Home
  