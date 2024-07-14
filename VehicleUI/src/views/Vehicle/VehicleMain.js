import React, { useEffect, useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardBody from "components/Card/CardBody.js";
import { Button } from "@material-ui/core";

// Config
import config from "../../config.js";

import axios from "axios";
import DataTable from "components/Table/DataTable.js";
import VehicleModal from "./VehicleModal.js";

const styles = {
  cardTitleWhite: {
    color: "#FFFFFF",
    marginTop: "0px",
    minHeight: "auto",
    fontWeight: "300",
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    marginBottom: "3px",
    textDecoration: "none",
    "& small": {
      color: "#777",
      fontSize: "65%",
      fontWeight: "400",
      lineHeight: "1",
    },
  },
};

const useStyles = makeStyles(styles);

export default function CarMain() {
  const [vehicleData, setVehicleData] = useState([]);
  const [models, setModels] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [editedVehicle, setEditedVehicle] = useState(undefined);
  const [removeSelected, setRemoveSelected] = useState(false);

  const classes = useStyles();

  //#region Actions

  const getBrands = () => {
    axios({
      method: "get",
      url: `${config.DefaultApiUrl}/Model/lov`,
      transformResponse: [
        (data) => {
          const res = JSON.parse(data);
          setModels(res);
        },
      ],
    });
  };

  const getVehicleListAction = () => {
    axios({
      method: "post",
      url: `${config.DefaultApiUrl}/Vehicle/all`,
      data: {},
      transformResponse: [
        (data) => {
          console.log({ data });
          setVehicleData(JSON.parse(data));
          setRemoveSelected(false);
        },
      ],
    });
  };

  const addOrUpdateVehicleActions = (data) => {
    const method = data.VehicleId ? "put" : "post";
    axios({
      method: method,
      url: `${config.DefaultApiUrl}/Vehicle`,
      data: data,
      transformResponse: [
        (res) => {
          const data = JSON.parse(res);
          if (data.success) {
            setShowModal(false);
            alert("Kayıt işlemi tamamlanmıştır");
            getVehicleListAction();
          } else {
            if (data.Message) {
              alert(data.Message);
              return;
            }
            alert("Bir hata oluştu");
          }
        },
      ],
    });
  };

  const deleteVehicleActions = (data) => {
    console.log({ data });
    axios({
      method: "delete",
      url: `${config.DefaultApiUrl}/vehicle/${data[0]}`,
      transformResponse: [
        () => {
          alert("Silme işlemi tamamlanmıştır");
          setRemoveSelected(true);
          getVehicleListAction();
        },
      ],
    });
  };

  //#endregion
  useEffect(() => {
    getVehicleListAction();
    getBrands();
  }, []);

  const newVehicle = () => {
    setEditedVehicle({
      Active: true,
      Name: "",
      VehicleId: undefined,
      Color: "",
      Plate: "",
      ModelId: "",
      ModelYear: "",
    });
    setShowModal(true);
  };

  const cells = [
    {
      id: "Name",
      numeric: false,
      disablePadding: true,
      label: "Vehicle Name",
      dataType: "text",
    },
    {
      id: "ModelId",
      numeric: false,
      disablePadding: false,
      label: "Model Id",
      dataType: "text",
    },
    {
      id: "ModelYear",
      numeric: false,
      disablePadding: false,
      label: "Model Year",
      dataType: "text",
    },
    {
      id: "Color",
      numeric: false,
      disablePadding: false,
      label: "Color",
      dataType: "text",
    },
    {
      id: "Plate",
      numeric: false,
      disablePadding: false,
      label: "Plate",
      dataType: "text",
    },
    {
      id: "Active",
      numeric: false,
      disablePadding: false,
      label: "Is Active",
      dataType: "boolean",
    },
  ];

  return (
    <GridContainer>
      <GridItem xs={12} sm={12} md={12}>
        <Card>
          <CardHeader color="primary">
            <p>
              Click to add a <Button onClick={newVehicle}>New Vehicle</Button>
            </p>
            <h4 className={classes.cardTitleWhite}>Vehicle Management</h4>
          </CardHeader>
          <CardBody>
            {vehicleData && (
              <DataTable
                headCells={cells}
                data={vehicleData}
                primaryId="VehicleId"
                handleEdit={(rowData) => {
                  setEditedVehicle(rowData);
                  setShowModal(true);
                }}
                handleDelete={(rows) => {
                  deleteVehicleActions(rows);
                }}
                removeSelected={removeSelected}
              />
            )}
          </CardBody>
        </Card>
      </GridItem>
      {showModal && (
        <VehicleModal
          open={showModal}
          detail={editedVehicle}
          handleClose={() => setShowModal(false)}
          addOrUpdateVehicle={(data) => addOrUpdateVehicleActions(data)}
          models={models}
        />
      )}
    </GridContainer>
  );
}
