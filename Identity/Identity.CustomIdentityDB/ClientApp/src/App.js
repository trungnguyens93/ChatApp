import "./App.css";
import SidePanel from "./components/SidePanel";
import Content from "./components/Content";
import { Provider } from "react-redux";
import configureStore from "./redux/configureStore";

const store = configureStore(window.__STATE__);

function App() {
  return (
    <div className="frame">
      <Provider store={store}>
        <SidePanel />

        <div></div>

        <Content />
      </Provider>
    </div>
  );
}

export default App;
