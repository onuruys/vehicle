import React, { useEffect, useState } from "react";
// @material-ui/core
import { makeStyles } from "@material-ui/core/styles";
import Icon from "@material-ui/core/Icon";
// @material-ui/icons
import Store from "@material-ui/icons/Store";
import DateRange from "@material-ui/icons/DateRange";
import LocalOffer from "@material-ui/icons/LocalOffer";
import Update from "@material-ui/icons/Update";
import TimeToLeave from "@material-ui/icons/TimeToLeave";
import Accessibility from "@material-ui/icons/Accessibility";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";

import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardIcon from "components/Card/CardIcon.js";
import CardFooter from "components/Card/CardFooter.js";

// Config
import config from "../../config.js";
import styles from "assets/jss/material-dashboard-react/views/dashboardStyle.js";
import axios from "axios";

const useStyles = makeStyles(styles);

export default function Dashboard() {
  const classes = useStyles();
  const [values, setValues] = useState({
    brandCount: 0,
    modelCount: 0,
    vehicleCount: 0,
  });

  // get brand count
  const getBrandCount = () => {
    axios({
      method: "get",
      url: `${config.DefaultApiUrl}/brand/lov`,
      transformResponse: [
        (data) => {
          console.log({ data });
          setValues((prev) => ({
            ...prev,
            brandCount: JSON.parse(data).length,
          }));
        },
      ],
    });
  };

  // get model count
  const getModelCount = () => {
    axios({
      method: "get",
      url: `${config.DefaultApiUrl}/model/lov`,
      transformResponse: [
        (data) => {
          setValues((prev) => ({
            ...prev,
            modelCount: JSON.parse(data).length,
          }));
        },
      ],
    });
  };

  // get vehicle count
  const getVehicleCount = () => {
    axios({
      method: "get",
      url: `${config.DefaultApiUrl}/vehicle/lov`,
      transformResponse: [
        (data) => {
          setValues((prev) => ({
            ...prev,
            vehicleCount: JSON.parse(data).length,
          }));
        },
      ],
    });
  };

  useEffect(() => {
    getBrandCount();
    getModelCount();
    getVehicleCount();
  }, []);

  return (
    <div>
      <GridContainer>
        <GridItem xs={12} sm={6} md={3}>
          <Card>
            <CardHeader color="warning" stats icon>
              <CardIcon color="warning">
                <Icon>content_copy</Icon>
              </CardIcon>
              <p className={classes.cardCategory}>Vehicle Count</p>
              <h3 className={classes.cardTitle}>{values.vehicleCount}</h3>
            </CardHeader>
            <CardFooter stats>
              <div className={classes.stats}>
                <TimeToLeave />
                <a href="/admin/vehicle">Manage Vehicle</a>
              </div>
            </CardFooter>
          </Card>
        </GridItem>
        <GridItem xs={12} sm={6} md={3}>
          <Card>
            <CardHeader color="success" stats icon>
              <CardIcon color="success">
                <Store />
              </CardIcon>
              <p className={classes.cardCategory}>Brand</p>
              <h3 className={classes.cardTitle}>{values.brandCount}</h3>
            </CardHeader>
            <CardFooter stats>
              <div className={classes.stats}>
                <DateRange />
                <a href="/admin/brand">Manage Brand</a>
              </div>
            </CardFooter>
          </Card>
        </GridItem>
        <GridItem xs={12} sm={6} md={3}>
          <Card>
            <CardHeader color="danger" stats icon>
              <CardIcon color="danger">
                <Icon>info_outline</Icon>
              </CardIcon>
              <p className={classes.cardCategory}>Model</p>
              <h3 className={classes.cardTitle}>{values.modelCount}</h3>
            </CardHeader>
            <CardFooter stats>
              <div className={classes.stats}>
                <LocalOffer />
                <a href="/admin/model">Manage Model</a>
              </div>
            </CardFooter>
          </Card>
        </GridItem>
        <GridItem xs={12} sm={6} md={3}>
          <Card>
            <CardHeader color="info" stats icon>
              <CardIcon color="info">
                <Accessibility />
              </CardIcon>
              <p className={classes.cardCategory}>Users</p>
              <h3 className={classes.cardTitle}>+245</h3>
            </CardHeader>
            <CardFooter stats>
              <div className={classes.stats}>
                <Update />
                Mange Users
              </div>
            </CardFooter>
          </Card>
        </GridItem>
      </GridContainer>
    </div>
  );
}
