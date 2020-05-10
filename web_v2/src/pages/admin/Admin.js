import React, { Component, Suspense } from "react";
import { Redirect, Route, Switch } from "react-router-dom";
import { Container } from "reactstrap";
import "react-toastify/dist/ReactToastify.min.css";
import cookie from "react-cookies";
import {
  AppAside,
  AppFooter,
  AppHeader,
  AppSidebar,
  AppSidebarFooter,
  AppSidebarForm,
  AppSidebarHeader,
  AppSidebarMinimizer,
  AppSidebarNav,
} from "@coreui/react";
// sidebar nav config
import navigation from "../../_nav";
// routes config
import routes from "../../routes";
import { getProfile } from "../../actions/profile.action";
import { connect } from "react-redux";

const DefaultAside = React.lazy(() => import("./DefaultLayout/DefaultAside"));
const DefaultFooter = React.lazy(() => import("./DefaultLayout/DefaultFooter"));
const DefaultHeader = React.lazy(() => import("./DefaultLayout/DefaultHeader"));

class DefaultLayout extends Component {
  loading = () => (
    <div className="animated fadeIn pt-1 text-center">Đang tải...</div>
  );

  signOut(e) {
    e.preventDefault();
    cookie.remove("token", { path: "/" });
    this.props.history.push("/login");
  }

  componentDidMount = () => {
    this.props.getProfile();
  };

  render() {
    const items = navigation.items;

    return (
      <>
        <div className="app">
          {/* <ToastContainer /> */}
          <AppHeader style={{ backgroundColor: "rgb(56,56,56)" }} fixed>
            <Suspense fallback={this.loading()}>
              <DefaultHeader onLogout={(e) => this.signOut(e)} />
            </Suspense>
          </AppHeader>
          <div className="app-body">
            <AppSidebar fixed display="lg">
              <AppSidebarHeader />
              <AppSidebarForm />
              <Suspense>
                <AppSidebarNav navConfig={{ items }} {...this.props} />
              </Suspense>
              <AppSidebarFooter />
              <AppSidebarMinimizer />
            </AppSidebar>

            <main className="main">
              {/* <AppBreadcrumb appRoutes={routes} /> */}
              <Container fluid>
                <Suspense fallback={this.loading()}>
                  <Switch>
                    {routes.map((route, idx) => {
                      return route.component ? (
                        <Route
                          key={idx}
                          path={route.path}
                          exact={route.exact}
                          name={route.name}
                          render={(props) => <route.component {...props} />}
                        />
                      ) : null;
                    })}
                    <Redirect from="/" to="/dashboard" />
                  </Switch>
                </Suspense>
              </Container>
            </main>

            <AppAside fixed>
              <Suspense fallback={this.loading()}>
                <DefaultAside />
              </Suspense>
            </AppAside>
          </div>

          <AppFooter>
            <Suspense fallback={this.loading()}>
              <DefaultFooter />
            </Suspense>
          </AppFooter>
        </div>
      </>
    );
  }
}

export default connect(
  (state) => ({
    profile: state.profile,
  }),
  { getProfile }
)(DefaultLayout);
