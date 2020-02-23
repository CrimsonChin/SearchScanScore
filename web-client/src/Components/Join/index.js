import React from 'react'

class Join extends React.Component {
    render() {
      return (
        <div>
          <h2>Join</h2>
          <p>Already Have your codes?  jump in and get involved!</p>
          <form>
          Game:<br/>
          <input type="text" name="gameId" /><br/>
          Team:<br/>
          <input type="text" name="teamId" /><br/>
          <button>Join</button><br/>
        </form>
        <button onClick={() => {
          //</div>withRouter.history.push('../Play')
          this.props.history.push("/Play/")
        }}>Join (Test)</button>
      </div>
      );
    }
  }

  export default Join
  